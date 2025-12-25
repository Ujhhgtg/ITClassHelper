using Serilog;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Ujhhgtg.Library.ExtensionMethods;

namespace ITClassHelper.Common.Messaging;

internal sealed class SimpleServer(int port)
{
    private readonly int _port = port;
    private TcpListener? _listener;
    private CancellationTokenSource? _cts;
    private Task? _acceptTask;

    private readonly ConcurrentDictionary<string, ClientSession> _clients = new();
    public ConcurrentDictionary<string, ClientSession> Clients => _clients;

    public void Start()
    {
        if (_listener != null)
            return;

        _cts = new CancellationTokenSource();

        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();
        Log.Information("SimpleServer listening on port {Port}", _port);

        _acceptTask = Task.Run(() => AcceptLoop(_cts.Token));
    }

    public void Stop()
    {
        Log.Information("SimpleServer stopping");

        if (_listener == null)
            return;

        _cts!.Cancel();

        try { _listener.Stop(); } catch { }

        foreach (var client in _clients.Values)
        {
            try { client.Client.Close(); } catch { }
        }

        try { _acceptTask?.Wait(); } catch { }

        _clients.Clear();
        _listener = null;
        _cts.Dispose();
        _cts = null;
        _acceptTask = null;

        Log.Information("SimpleServer stopped");
    }

    private async Task AcceptLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            TcpClient client;

            try
            {
                Log.Debug("Waiting for client connection...");
                client = await _listener!.AcceptTcpClientAsync(token);
                Log.Information(
                    "Client connected from {RemoteEndPoint}",
                    client.Client.RemoteEndPoint);
            }
            catch
            {
                Log.Warning("Accept loop terminated");
                break;
            }

            _ = Task.Run(() => ClientLoop(client, token), token);
        }
    }

    private async Task ClientLoop(TcpClient client, CancellationToken token)
    {
        Log.Information(
            "Client session started: {RemoteEndPoint}",
            client.Client.RemoteEndPoint);

        var stream = client.GetStream();

        string? clientId = null;

        // first packet MUST be ClientHello
        var (type, payload) = await PacketIo.ReceiveAsync(stream, token);

        if (type != PacketType.ClientHello)
            throw new InvalidOperationException("Client did not send ClientHello");

        clientId = Encoding.UTF8.GetString(payload);

        var session = new ClientSession
        {
            ClientId = clientId,
            Client = client
        };

        _clients.AddOrUpdate(
            clientId,
            session,
            (_, old) =>
            {
                try { old.Client.Close(); } catch { }
                return session;
            });

        var ip = session.RemoteEndPoint.Address.ToString();
        Log.Information(
            "Client registered. ClientId={ClientId}, IP={IP}, Port={Port}",
            clientId,
            ip,
            session.RemoteEndPoint.Port);

        try
        {
            while (!token.IsCancellationRequested)
            {
                (type, payload) = await PacketIo.ReceiveAsync(stream, token);

                switch (type)
                {
                    case PacketType.Heartbeat:
                        Log.Debug(
                            "Heartbeat received from {RemoteEndPoint}",
                            client.Client.RemoteEndPoint);
                        break;

                    case PacketType.CommandAck:
                        Log.Information(
                            "Command ACK from {RemoteEndPoint}: {Message}",
                            client.Client.RemoteEndPoint,
                            Encoding.UTF8.GetString(payload));
                        break;

                    case PacketType.CommandResult:
                        Log.Information(
                            "Command RESULT from {RemoteEndPoint}: {Result}",
                            client.Client.RemoteEndPoint,
                            Encoding.UTF8.GetString(payload));
                        break;

                    case PacketType.ClientToClient:
                        HandleClientToClient(session, payload);
                        break;
                }

                Log.Information("CLIENT → {Type}: {Packet}", type, Encoding.UTF8.GetString(payload));
            }
        }
        catch (Exception ex)
        {
            Log.Warning(
                ex,
                "Client connection error: {RemoteEndPoint}",
                client.Client.RemoteEndPoint);

        }
        finally
        {
            Log.Information(
                "Client disconnected: {RemoteEndPoint}",
                client.Client.RemoteEndPoint);
            if (clientId != null)
            {
                _clients.TryRemove(clientId, out _);
                Log.Information(
                    "Client removed. ClientId={ClientId}",
                    clientId);
            }
            try { client.Close(); } catch { }
        }
    }

    private void HandleClientToClient(
    ClientSession sender,
    byte[] payload)
    {
        using var ms = new MemoryStream(payload);

        Span<byte> lenBuf = stackalloc byte[4];
        ms.ReadExactly(lenBuf);
        int targetLen = BitConverter.ToInt32(lenBuf);

        byte[] targetBytes = new byte[targetLen];
        ms.ReadExactly(targetBytes);

        string targetClientId = Encoding.UTF8.GetString(targetBytes);
        byte[] innerPayload = ms.ReadAllBytes();

        if (!_clients.TryGetValue(targetClientId, out var target))
        {
            SendC2CResult(sender, "TARGET_OFFLINE");
            return;
        }

        if (!CanClientCommunicate(sender.ClientId, targetClientId, out var reason))
        {
            SendC2CResult(sender, reason ?? "DENIED");
            return;
        }

        // build forwarded payload
        var fromBytes = Encoding.UTF8.GetBytes(sender.ClientId);

        using var outMs = new MemoryStream();
        outMs.Write(BitConverter.GetBytes(fromBytes.Length));
        outMs.Write(fromBytes);
        outMs.Write(innerPayload);

        try
        {
            PacketIo.Send(
                target.Client.GetStream(),
                PacketType.ClientToClient,
                outMs.ToArray());

            SendC2CResult(sender, "DELIVERED");

            Log.Information(
                "C2C forwarded: {From} → {To} ({Size} bytes)",
                sender.ClientId,
                targetClientId,
                innerPayload.Length);
        }
        catch (Exception ex)
        {
            Log.Warning(ex,
                "C2C delivery failed: {From} → {To}",
                sender.ClientId,
                targetClientId);

            SendC2CResult(sender, "FAILED");
        }
    }

    private static void SendC2CResult(ClientSession sender, string result)
    {
        PacketIo.Send(
            sender.Client.GetStream(),
            PacketType.ClientToClientResult,
            Encoding.UTF8.GetBytes(result));
    }

    public void SendMessage(string message)
    {
        Log.Information("Broadcasting message to {ClientCount} clients", _clients.Count);
        Broadcast(PacketType.Message, Encoding.UTF8.GetBytes(message));
    }

    public void SendCommand(string command)
    {
        Log.Information("Broadcasting command: {Command}", command);
        Broadcast(PacketType.Command, Encoding.UTF8.GetBytes(command));
    }

    public void SendFile(string path)
    {
        Log.Information("Broadcasting file: {Path}", path);

        var name = Path.GetFileName(path);
        var nameBytes = Encoding.UTF8.GetBytes(name);
        var fileBytes = File.ReadAllBytes(path);

        using var ms = new MemoryStream();
        ms.Write(BitConverter.GetBytes(nameBytes.Length));
        ms.Write(nameBytes);
        ms.Write(fileBytes);

        Broadcast(PacketType.File, ms.ToArray());
        Log.Information("File broadcast completed: {FileName}", name);
    }

    private void Broadcast(PacketType type, byte[] payload)
    {
        foreach (var client in _clients)
        {
            try
            {
                PacketIo.Send(client.Value.Client.GetStream(), type, payload);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Failed to send packet to a client");
            }
        }
    }

    private bool TrySendToClient(string clientId, PacketType type, byte[] payload)
    {
        if (!_clients.TryGetValue(clientId, out var session))
            return false;

        try
        {
            PacketIo.Send(session.Client.GetStream(), type, payload);
            return true;
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Failed to send to ClientId={ClientId}", clientId);
            return false;
        }
    }

    public bool SendMessageTo(string clientId, string message)
    {
        return TrySendToClient(
            clientId,
            PacketType.Message,
            Encoding.UTF8.GetBytes(message));
    }

    public bool SendCommandTo(string clientId, string command)
    {
        return TrySendToClient(
            clientId,
            PacketType.Command,
            Encoding.UTF8.GetBytes(command));
    }

    public bool SendFileTo(string clientId, string path)
    {
        var name = Path.GetFileName(path);
        var nameBytes = Encoding.UTF8.GetBytes(name);
        var fileBytes = File.ReadAllBytes(path);

        using var ms = new MemoryStream();
        ms.Write(BitConverter.GetBytes(nameBytes.Length));
        ms.Write(nameBytes);
        ms.Write(fileBytes);

        return TrySendToClient(
            clientId,
            PacketType.File,
            ms.ToArray());
    }

    public bool TryGetClientIp(string clientId, out IPAddress? ip)
    {
        ip = null;

        if (!_clients.TryGetValue(clientId, out var session))
            return false;

        ip = session.RemoteEndPoint.Address;
        return true;
    }

    private static bool CanClientCommunicate(
        string fromClientId,
        string toClientId,
        out string? reason)
    {
        if (fromClientId == toClientId)
        {
            reason = "SELF_NOT_ALLOWED";
            return false;
        }

        reason = null;
        return true; // allow by default
    }
}
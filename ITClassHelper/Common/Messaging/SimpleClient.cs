using Serilog;
using System.Net.Sockets;
using System.Text;
using Ujhhgtg.Library.ExtensionMethods;

namespace ITClassHelper.Common.Messaging;

public sealed class SimpleClient
{
    private TcpClient? _client;
    private string _host = "";
    private int _port;

    private readonly string _clientId;

    private CancellationTokenSource? _cts;
    private Task? _mainTask;

    private bool _running;

    public SimpleClient()
    {
        _clientId = LoadOrCreateClientId();
    }

    private static string LoadOrCreateClientId()
    {
        if (Const.ClientIdPath.Exists)
            return File.ReadAllText(Const.ClientIdPath).Trim();

        var id = Guid.NewGuid().ToString("N");
        File.WriteAllText(Const.ClientIdPath, id);
        return id;
    }

    public bool Running => _running;

    public void Start(string host, int port)
    {
        if (_mainTask != null)
            return;

        _host = host;
        _port = port;

        _cts = new CancellationTokenSource();
        _running = true;
        Log.Information("SimpleClient starting. Target={Host}:{Port}", host, port);

        _mainTask = Task.Run(() => RunAsync(_cts.Token))
                        .ContinueWith(t =>
                        {
                            // swallow cancellation
                        });
    }

    public void Stop()
    {
        Log.Information("SimpleClient stopping");

        if (_cts == null)
            return;

        _running = false;
        _cts.Cancel();

        try
        {
            _client?.Close();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled client error");
        }

        try
        {
            _mainTask?.Wait();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled client error");
        }

        _client = null;
        _cts.Dispose();
        _cts = null;
        _mainTask = null;

        Log.Information("SimpleClient stopped");
    }

    private async Task RunAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                _client = new TcpClient();
                Log.Information("Connecting to server {Host}:{Port}", _host, _port);
                await _client.ConnectAsync(_host, _port, token);
                Log.Information("Connected to server");

                var stream = _client.GetStream();

                PacketIo.Send(
                    stream,
                    PacketType.ClientHello,
                    Encoding.UTF8.GetBytes(_clientId)
                );
                Log.Information("Sent ClientHello with ClientId={ClientId}", _clientId);

                var hbTask = HeartbeatLoop(stream, token);
                var rxTask = ReceiveLoop(stream, token);

                await Task.WhenAny(hbTask, rxTask);
            }
            catch
            {
                Log.Warning("Connection lost. Reconnecting in 3 seconds...");

                if (!token.IsCancellationRequested)
                    await Task.Delay(3000, token);
            }
            finally
            {
                Log.Information("Connection closed");

                try { _client?.Close(); }
                catch (Exception ex)
                {
                    Log.Error(ex, "Unhandled client error");
                }

                _client = null;
                _running = false;
            }
        }
    }

    private async Task ReceiveLoop(NetworkStream stream, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var (type, payload) = await PacketIo.ReceiveAsync(stream, token);
            Log.Information("Received packet {Type} ({Size} bytes)", type, payload.Length);

            switch (type)
            {
                case PacketType.Message:
                    Console.WriteLine("MSG: " + Encoding.UTF8.GetString(payload));
                    break;

                case PacketType.Command:
                    await HandleCommand(stream, payload, token);
                    break;

                case PacketType.File:
                    ReceiveFile(payload);
                    break;

                case PacketType.ClientToClient:
                    {
                        using var ms = new MemoryStream(payload);

                        Span<byte> lenBuf = stackalloc byte[4];
                        ms.ReadExactly(lenBuf);
                        int fromLen = BitConverter.ToInt32(lenBuf);

                        byte[] fromBytes = new byte[fromLen];
                        ms.ReadExactly(fromBytes);

                        string fromClientId = Encoding.UTF8.GetString(fromBytes);
                        byte[] innerPayload = ms.ReadAllBytes();

                        Log.Information(
                            "Message from ClientId={ClientId}, {Size} bytes",
                            fromClientId,
                            innerPayload.Length);

                        break;
                    }

                case PacketType.ClientToClientResult:
                    {
                        Log.Information(
                            "Client-to-client result: {Result}",
                            Encoding.UTF8.GetString(payload));
                        break;
                    }
            }
        }
    }

    private static async Task HandleCommand(
        NetworkStream stream,
        byte[] payload,
        CancellationToken token)
    {
        var command = Encoding.UTF8.GetString(payload);
        Log.Information("Command received: {Command}", command);

        PacketIo.Send(
            stream,
            PacketType.CommandAck,
            Encoding.UTF8.GetBytes($"ACK:{command}")
        );

        string result;
        try
        {
            await Task.Delay(500, token);
            result = $"SUCCESS:{command}";
            Log.Information("Command executed successfully: {Command}", command);
        }
        catch (Exception ex)
        {
            result = $"ERROR:{ex.Message}";
            Log.Error(ex, "Command execution failed: {Command}", command);
        }

        PacketIo.Send(
            stream,
            PacketType.CommandResult,
            Encoding.UTF8.GetBytes(result)
        );
    }

    private void ReceiveFile(byte[] payload)
    {
        using var ms = new MemoryStream(payload);

        Span<byte> lenBuf = stackalloc byte[4];
        ms.ReadExactly(lenBuf);

        int nameLen = BitConverter.ToInt32(lenBuf);
        byte[] nameBytes = new byte[nameLen];
        ms.ReadExactly(nameBytes);

        string name = Encoding.UTF8.GetString(nameBytes);
        string path = Path.Combine(Environment.CurrentDirectory, name);

        using var fs = File.Create(path);
        ms.CopyTo(fs);

        Log.Information("File received: {Path}", path);
    }

    private static async Task HeartbeatLoop(NetworkStream stream, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            PacketIo.Send(stream, PacketType.Heartbeat, []);
            Log.Debug("Heartbeat sent");
            await Task.Delay(5000, token);
        }
    }

    public void SendToClient(string targetClientId, byte[] data)
    {
        if (_client == null)
            throw new InvalidOperationException("Client not connected");

        var idBytes = Encoding.UTF8.GetBytes(targetClientId);

        using var ms = new MemoryStream();
        ms.Write(BitConverter.GetBytes(idBytes.Length));
        ms.Write(idBytes);
        ms.Write(data);

        PacketIo.Send(
            _client.GetStream(),
            PacketType.ClientToClient,
            ms.ToArray());
    }

    public void SendMessageToClient(string targetClientId, string message)
    {
        SendToClient(
            targetClientId,
            Encoding.UTF8.GetBytes(message));
    }

    public void SendCommandToClient(string targetClientId, string command)
    {
        SendToClient(
            targetClientId,
            Encoding.UTF8.GetBytes(command));
    }

    public void SendFileToClient(string targetClientId, string path)
    {
        var name = Path.GetFileName(path);
        var nameBytes = Encoding.UTF8.GetBytes(name);
        var fileBytes = File.ReadAllBytes(path);

        using var ms = new MemoryStream();
        ms.Write(BitConverter.GetBytes(nameBytes.Length));
        ms.Write(nameBytes);
        ms.Write(fileBytes);

        SendToClient(targetClientId, ms.ToArray());
    }
}

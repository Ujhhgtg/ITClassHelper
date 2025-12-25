using System.Net.Sockets;

namespace ITClassHelper.Common.Messaging;

internal static class PacketIo
{
    public static void Send(NetworkStream stream, PacketType type, byte[] payload)
    {
        Span<byte> header = stackalloc byte[5];
        header[0] = (byte)type;
        BitConverter.TryWriteBytes(header[1..], payload.Length);

        stream.Write(header);
        if (payload.Length > 0)
            stream.Write(payload);

        stream.Flush();
    }

    public static (PacketType type, byte[] payload) Receive(NetworkStream stream)
    {
        Span<byte> header = stackalloc byte[5];
        ReadExactly(stream, header);

        var type = (PacketType)header[0];
        int len = BitConverter.ToInt32(header[1..]);

        byte[] payload = len > 0 ? new byte[len] : Array.Empty<byte>();
        if (len > 0)
            ReadExactly(stream, payload);

        return (type, payload);
    }

    private static void ReadExactly(NetworkStream stream, Span<byte> buffer)
    {
        int offset = 0;
        while (offset < buffer.Length)
        {
            int read = stream.Read(buffer[offset..]);
            if (read == 0)
                throw new IOException("Remote closed connection");
            offset += read;
        }
    }

    public static async Task SendAsync(
        NetworkStream stream,
        PacketType type,
        byte[] payload,
        CancellationToken token = default)
    {
        byte[] header = new byte[5];
        header[0] = (byte)type;
        BitConverter.TryWriteBytes(header.AsSpan(1), payload.Length);

        await stream.WriteAsync(header, token);
        if (payload.Length > 0)
            await stream.WriteAsync(payload, token);

        await stream.FlushAsync(token);
    }

    public static async Task<(PacketType type, byte[] payload)> ReceiveAsync(
        NetworkStream stream,
        CancellationToken token = default)
    {
        byte[] header = new byte[5];
        await ReadExactlyAsync(stream, header, token);

        var type = (PacketType)header[0];
        int len = BitConverter.ToInt32(header, 1);

        byte[] payload = len > 0 ? new byte[len] : Array.Empty<byte>();
        if (len > 0)
            await ReadExactlyAsync(stream, payload, token);

        return (type, payload);
    }

    private static async Task ReadExactlyAsync(
        NetworkStream stream,
        byte[] buffer,
        CancellationToken token)
    {
        int offset = 0;
        while (offset < buffer.Length)
        {
            int read = await stream.ReadAsync(
                buffer.AsMemory(offset),
                token);

            if (read == 0)
                throw new IOException("Remote closed connection");

            offset += read;
        }
    }
}
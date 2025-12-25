using System.Net;
using System.Net.Sockets;

namespace ITClassHelper.Common.Messaging;

internal sealed class ClientSession
{
    public string ClientId = "";
    public TcpClient Client = null!;
    public IPEndPoint RemoteEndPoint =>
        (IPEndPoint)Client.Client.RemoteEndPoint!;
}

namespace ITClassHelper.Common.Messaging;

// [1 byte ] PacketType
// [4 bytes] Payload length (int32 LE)
// [n bytes] Payload

// [4 bytes] targetClientIdLength (int32)
// [n bytes] targetClientId (UTF-8)
// [m bytes] message payload (opaque)

internal enum PacketType : byte
{
    Message = 1,
    Command = 2,
    File = 3,

    CommandAck = 4, // client → server
    CommandResult = 5, // client → server

    Heartbeat = 6,  // bidirectional
    ClientHello = 7,

    ClientToClient = 8,      // client → server
    ClientToClientResult = 9 // server → client
}

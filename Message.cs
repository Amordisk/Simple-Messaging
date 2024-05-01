using System.Runtime.CompilerServices;

namespace SimpleMessager;

public enum MessageType
{
    UserMessage,
    UserFileMessage,
    KeepAlive,
    ServerMessage,
    Handshake,
    Keys
}

public class Message
{
    public string message { get; set; }
    public string? username { get; set; }
    public MessageType messageType { get; set; }
    public DateTime dateTime { get; set; }

    public Message(string message, string? username, MessageType messageType)
    {
        this.message = message;
        this.username = username;
        this.messageType = messageType;
    }
}

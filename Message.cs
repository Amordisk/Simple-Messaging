using System.Runtime.CompilerServices;

namespace SimpleMessager;

public enum messageType
{
    UserMessage,
    KeepAlive,
    ServerMessage,
    Handshake
}

public class Message
{
    private string message { get; set; }
    private string username { get; set; }
    private messageType messageType { get; set; }
    private DateTime dateTime { get; set; }

    public Message(string message, string username, messageType messageType)
    {
        this.message = message;
        this.username = username;
        this.messageType = messageType;
    }
}

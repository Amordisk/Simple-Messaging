using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMessager;

public class MessageHandler
{
    private Message message { get; set; }
    public MessageHandler(Message message)
    {
        this.message = message;
    }
    public void messageSorter()
    {
        switch (message.messageType)
        {
            case MessageType.UserMessage:

                Console.WriteLine(message.message);
                break;
            case MessageType.UserFileMessage:
                break;
            case MessageType.KeepAlive:
                break;
            case MessageType.ServerMessage:
                break;
            case MessageType.Handshake:
                break;
            case MessageType.Keys:
                break;
        }
    }
}
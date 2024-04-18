using System.Net.Sockets;
using System.Text;

namespace SimpleMessager;

public class Sender
{
    private TcpClient sidedness { get; set; }

    public Sender(TcpClient sidedness)
    {
        this.sidedness = sidedness;
    }

    void sendMessageConsole(TcpClient sidedness)
    {
        var message = Console.ReadLine();
        sendMessageBackground(sidedness, message);
    }
    void sendMessageBackground(TcpClient sidedness, String message)
    {
        NetworkStream stream = sidedness.GetStream();
        var messageBytes = Encoding.UTF8.GetBytes(message);
        stream.Write(messageBytes);
    }
}
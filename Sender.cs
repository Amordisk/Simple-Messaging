using System.Net.Sockets;
using System.Text;

namespace SimpleMessager;

public class Sender
{
    private TcpClient sidedness { get; set; }
    private NetworkStream stream { get; set; }

    public Sender(TcpClient sidedness)
    {
        this.sidedness = sidedness;
        stream = sidedness.GetStream();
    }

    public void sendMessageConsole()
    {
        var message = Console.ReadLine();
        sendMessageBackground(message);
    }
    public void sendMessageBackground(String message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        stream.Write(messageBytes);
    }
}
using System.Net.Sockets;
using System.Text;

namespace SimpleMessager;

public class Sender
{
    private TcpClient sidedness { get; set; }
    private NetworkStream stream { get; set; }
    private string? username { get; set; }

    public Sender(TcpClient sidedness, string? username)
    {
        this.sidedness = sidedness;
        stream = sidedness.GetStream();
        this.username = username;
    }

    public void sendMessageConsole()
    {
        var time = DateTime.Now.ToShortTimeString();
        var message = $"{username} " + time + " >| " + Console.ReadLine();
        sendMessageBackground(message);
    }
    public void sendMessageBackground(String message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        stream.Write(messageBytes);
    }
}
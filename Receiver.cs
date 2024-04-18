using System.Net.Sockets;
using System.Text;

namespace SimpleMessager;

public class Receiver
{
       private TcpClient sidedness { get; set; }

    public Receiver(TcpClient sidedness)
    {
        this.sidedness = sidedness;
    }

    void receiveMessageConsole(TcpClient sidedness)
    {
        string message = receiveMessageBackground(sidedness);
        Console.WriteLine($">| {message}");
    }
    public string receiveMessageBackground(TcpClient sidedness)
    {
        NetworkStream stream = sidedness.GetStream();
        var buffer = new byte[1024];
        int received = stream.Read(buffer);
        var message = Encoding.UTF8.GetString(buffer, 0, received);
        return message;
    }
}
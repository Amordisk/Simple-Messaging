using System.Net.Sockets;
using System.Text;

namespace SimpleMessager;

public class Receiver
{
       private TcpClient sidedness { get; set; }
       private NetworkStream stream { get; set; }

    public Receiver(TcpClient sidedness)
    {
        this.sidedness = sidedness;
        stream = sidedness.GetStream();
    }

    
    public void receiveMessageConsole()
    {
        string message = receiveMessageBackground();
        Console.WriteLine(message);
    }
    public string receiveMessageBackground()
    {
        var buffer = new byte[1024];
        int received = stream.Read(buffer);
        var message = Encoding.UTF8.GetString(buffer, 0, received);
        return message;
    }
}
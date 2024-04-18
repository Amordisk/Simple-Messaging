using System.Net.Sockets;

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

    }
    void receiveMessageBackground(TcpClient sidedness, String message)
    {

    }
}
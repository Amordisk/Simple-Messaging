using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SimpleMessager;

IPAddress? ipAddress;
string ipInput;
int port;
string portInput;
string hostOrClient;
string username;

Console.WriteLine("Initiate as a Host or Client: ");
hostOrClient = Console.ReadLine();
Console.WriteLine("Username: ");
username = Console.ReadLine();
do
{
    Console.Write("Please enter a valid IP-address: ");
    ipInput = Console.ReadLine();
}while(!IPAddress.TryParse(ipInput, out ipAddress));
do
{
    Console.WriteLine("Please enter a valid port: ");
    portInput = Console.ReadLine();
}while(!(int.TryParse(portInput, out port)&& port > 0 && port < 65535));

var ipEndPoint = new IPEndPoint(ipAddress, port);
Console.WriteLine($"is {ipEndPoint} the correct ip address?");

if (hostOrClient == "Client")
{
    TcpClient client = new();
    client.Connect(ipEndPoint);

    var sendThread = new Thread(() => sendUserMessage(client));
    var receiveThread = new Thread(() => receiveUserMessage(client));

    sendThread.Start();
    receiveThread.Start();
    sendUserMessage(client);

}
else if (hostOrClient == "Host")
{
    TcpListener listener = new(ipEndPoint);
    

    try
    {
        listener.Start();

        TcpClient handler = listener.AcceptTcpClient();
  
        var sendThread = new Thread(() => sendUserMessage(handler));
        var receiveThread = new Thread(() => receiveUserMessage(handler));

        sendThread.Start();
        receiveThread.Start();
    }
    finally
    {
        listener.Stop();
    }
}
void sendUserMessage(TcpClient sidedness)
{
    var sender = new Sender(sidedness);
    while(true)
    {
        sender.sendMessageConsole();
    }
}
void receiveUserMessage(TcpClient sidedness)
{
    var receiver = new Receiver(sidedness);
    while(true)
    {
        receiver.receiveMessageConsole();
    }
}

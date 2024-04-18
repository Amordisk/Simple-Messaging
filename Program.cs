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

    var sendThread = new Thread(() => sendMessage(client));
    var receiveThread = new Thread(() => receiveMessage(client));

    sendThread.Start();
    receiveThread.Start();
    sendMessage(client);

}
else if (hostOrClient == "Host")
{
    TcpListener listener = new(ipEndPoint);
    

    try
    {
        listener.Start();

        TcpClient handler = listener.AcceptTcpClient();
  
        var sendThread = new Thread(() => sendMessage(handler));
        var receiveThread = new Thread(() => receiveMessage(handler));

        sendThread.Start();
        receiveThread.Start();
    }
    finally
    {
        listener.Stop();
    }
}
void sendMessage(TcpClient sidedness)
{
    using NetworkStream stream = sidedness.GetStream();
    while(true)
    {
        var timeSent = DateTime.Now.ToShortTimeString();
        var message = Console.ReadLine();
        var messageBytes = Encoding.UTF8.GetBytes(message);
        stream.Write(messageBytes);
    }
}
void receiveMessage(TcpClient sidedness)
{
    using NetworkStream stream = sidedness.GetStream();
    while(true)
    {
        var buffer = new byte[1024];
        int received = stream.Read(buffer);
        var message = Encoding.UTF8.GetString(buffer, 0, received);
        Console.WriteLine($">| {message}");
    }
}

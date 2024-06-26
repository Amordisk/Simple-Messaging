﻿using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using SimpleMessager;

IPAddress? ipAddress;
string? ipInput;
int port;
string? portInput;
string? hostOrClient;
string? username;

Console.WriteLine("Initiate as a Host or Client: ");
hostOrClient = Console.ReadLine();
if (string.IsNullOrEmpty(hostOrClient)) hostOrClient = "Host";
Console.WriteLine("Username: ");
username = Console.ReadLine();
if (string.IsNullOrEmpty(username)) username = "Anonymous";
do
{
    Console.Write("Please enter a valid IP-address: ");
    ipInput = Console.ReadLine();
    if (string.IsNullOrEmpty(ipInput)) ipInput = "127.0.0.1";
}while(!IPAddress.TryParse(ipInput, out ipAddress));
do
{
    Console.WriteLine("Please enter a valid port: ");
    portInput = Console.ReadLine();
    if (string.IsNullOrEmpty(portInput)) portInput = "5001";
}while(!(int.TryParse(portInput, out port)&& port > 0 && port < 65535));

var ipEndPoint = new IPEndPoint(ipAddress, port);
Console.WriteLine($"is {ipEndPoint} the correct ip address?");

if (hostOrClient == "Client")
{
    TcpClient client = new();
    client.Connect(ipEndPoint);

    var sendThread = new Thread(() => sendUserMessage(client));
    var receiveThread = new Thread(() => receiveMessage(client));

    sendThread.Start();
    receiveThread.Start();

}
else if (hostOrClient == "Host")
{
    TcpListener listener = new(ipEndPoint);
    

    try
    {
        listener.Start();

        TcpClient handler = listener.AcceptTcpClient();
  
        var sendThread = new Thread(() => sendUserMessage(handler));
        var receiveThread = new Thread(() => receiveMessage(handler));

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
    var sender = new Sender(sidedness, username);
    while(true)
    {
        sender.sendMessageConsole();
    }
}
void receiveMessage(TcpClient sidedness)
{
    var receiver = new Receiver(sidedness);
    while(true)
    {
        string message = receiver.receiveMessageBackground();
        var messageobject = new Message(message, username, MessageType.UserMessage);
        var MessageHandler = new MessageHandler(messageobject);
        MessageHandler.messageSorter();
    }
}

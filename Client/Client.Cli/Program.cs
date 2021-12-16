// See https://aka.ms/new-console-template for more information
using NetworkInfomation;
using System.Net;
using System.Net.Sockets;


var host = Dns.GetHostEntry(Dns.GetHostName());
var ip = host.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
if (ip is null)
{
    Console.WriteLine("No ip address, exit.");
    return;
}

var port = 32333;
Console.WriteLine("Your netcode is:");
Console.WriteLine(new NetCode(ip.ToString(), (ushort)port).Encode());
Console.WriteLine("Please input a netcode or -1 to wait others connect");
var code = Console.ReadLine();

using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
{
    if (code != "-1" && code is not null)
    {
        var info = NetCode.Decode(code);
        var endPoint = new IPEndPoint(IPAddress.Parse(info.IpAddress), info.Port);
    
        socket.Connect(endPoint);

        
    }



    // socket.Bind(endPoint);
    Console.WriteLine("Now binding...");


    socket.Listen();
}
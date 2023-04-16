using System.Net;
using System.Net.Sockets;
using System.Text;

IPHostEntry host = Dns.GetHostEntry("localhost");
IPAddress ip = host.AddressList[0];
IPEndPoint localEndPoint = new IPEndPoint(ip, 11000);

Socket client = new Socket(ip.AddressFamily, 
                             SocketType.Stream, 
                             ProtocolType.Tcp);

client.Connect(localEndPoint);

string? message = Console.ReadLine();
byte[] messageSent = Encoding.ASCII.GetBytes(message);
while (message != "!end")
{
    int byteSent = client.Send(messageSent);
    byte[] messageReceived = new byte[1024];
    int byteRecv = client.Receive(messageReceived);
    Console.WriteLine("Server: " + Encoding.ASCII.GetString(messageReceived, 0, byteRecv));
    message = Console.ReadLine();
    messageSent = Encoding.ASCII.GetBytes(message);
}
try {
    client.Shutdown(SocketShutdown.Both);
    client.Close();
}
catch{}
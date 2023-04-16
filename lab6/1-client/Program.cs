using System.Net;
using System.Net.Sockets;
using System.Text;

IPHostEntry host = Dns.GetHostEntry("localhost");
IPAddress ip = host.AddressList[0];
IPEndPoint localEndPoint = new IPEndPoint(ip, 6789);

Socket clientSocket = new Socket(ip.AddressFamily,
                             SocketType.Stream, 
                             ProtocolType.Tcp);

clientSocket.Connect(localEndPoint);
string message = "Witaj serwerze!";
byte[] msg = Encoding.UTF8.GetBytes(message);
clientSocket.Send(msg);
byte[] buffer = new byte[1024];
int bytesRec = clientSocket.Receive(buffer);
string reply = Encoding.UTF8.GetString(buffer, 0, bytesRec);
Console.WriteLine("Otrzymano: \n" + reply);
try {
    clientSocket.Shutdown(SocketShutdown.Both);
    clientSocket.Close();
}
catch{}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

IPHostEntry host = Dns.GetHostEntry("localhost");
IPAddress ip = host.AddressList[0];
IPEndPoint localEndPoint = new IPEndPoint(ip, 6789);

Socket serwerSocket = new Socket(ip.AddressFamily,
                             SocketType.Stream, 
                             ProtocolType.Tcp);

serwerSocket.Bind(localEndPoint);
serwerSocket.Listen(10);

Socket clientSocket = serwerSocket.Accept();
byte[] buffer = new byte[1024];
int bytesRec = clientSocket.Receive(buffer);
String message = Encoding.UTF8.GetString(buffer, 0, bytesRec);
Console.WriteLine("Otrzymano: " + message);
string reply = "Odebrano wiadomość: \n" + message;
byte[] msg = Encoding.UTF8.GetBytes(reply);
clientSocket.Send(msg);
try {
    serwerSocket.Shutdown(SocketShutdown.Both);
    serwerSocket.Close();
}
catch{}
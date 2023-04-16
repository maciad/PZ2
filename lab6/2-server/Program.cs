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
byte[] size = new byte[4];
clientSocket.Receive(size);
int sizeInt = BitConverter.ToInt32(size, 0);

byte[] buffer = new byte[sizeInt];
int bytesRec = clientSocket.Receive(buffer);
String message = Encoding.UTF8.GetString(buffer, 0, bytesRec);
Console.WriteLine("Otrzymano: " + message);

string reply = "Odebrano wiadomość: \n" + message;

byte[] replyBytes = Encoding.UTF8.GetBytes(reply);
int replySize = replyBytes.Length;
byte[] replySizeBytes = BitConverter.GetBytes(replySize);
clientSocket.Send(replySizeBytes);
clientSocket.Send(replyBytes);
try {
    serwerSocket.Shutdown(SocketShutdown.Both);
    serwerSocket.Close();
}
catch{}
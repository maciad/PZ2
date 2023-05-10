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
int size = msg.Length;
byte[] sizeMsg = BitConverter.GetBytes(size);
clientSocket.Send(sizeMsg);
clientSocket.Send(msg);

byte[] sizeMsg2 = new byte[4];
clientSocket.Receive(sizeMsg2);
int size2 = BitConverter.ToInt32(sizeMsg2, 0);
byte[] msg2 = new byte[size2];
clientSocket.Receive(msg2);
string reply = Encoding.UTF8.GetString(msg2);

Console.WriteLine("Otrzymano: \n" + reply);
try {
    clientSocket.Shutdown(SocketShutdown.Both);
    clientSocket.Close();
}
catch{}

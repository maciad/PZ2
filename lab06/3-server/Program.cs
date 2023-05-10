using System.Net;
using System.Net.Sockets;
using System.Text;

string show(string path)
{
    string response = "";
    string[] dirs = Directory.GetDirectories(path);
    string[] files = Directory.GetFiles(path);

    foreach (string dir in dirs)
    {
        response += dir + "\n";
    }
    return response;
}

IPHostEntry host = Dns.GetHostEntry("localhost");
IPAddress ip = host.AddressList[0];
IPEndPoint localEndPoint = new IPEndPoint(ip, 11000);

Socket server = new Socket(ip.AddressFamily,
                           SocketType.Stream, 
                           ProtocolType.Tcp);

server.Bind(localEndPoint);
server.Listen(10);
Socket client = server.Accept();

byte[] bytes = new byte[1024];
string response;

bool end = false;
while(!end)
{
    string currentPath = Directory.GetCurrentDirectory();
    int bytesRec = client.Receive(bytes);
    string command = Encoding.ASCII.GetString(bytes, 0, bytesRec);

    if (command == "!end")
    {
        end = true;
    }
    else if (command == "list")
    {
        response = show(currentPath);
        byte[] msg = Encoding.ASCII.GetBytes(response);
        client.Send(msg);
    }
    else if (command.StartsWith("in "))
    {
        //TODO
    }
    else
    {
        response = "nieznane polecenie";
        byte[] msg = Encoding.ASCII.GetBytes(response);
        client.Send(msg);
    }
}
try {
    server.Shutdown(SocketShutdown.Both);
    server.Close();
}
catch{}
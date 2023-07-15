using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;
class TCPServer
{
    static void Main()
    {
        TcpListener tcpserver = null;
        try
        {
            IPAddress ipaddress = IPAddress.Parse("127.0.0.1");
            int port = 7070;


            tcpserver = new TcpListener(ipaddress, port);
            tcpserver.Start();
            WriteLine("Waiting for a connections...");


            TcpClient client = tcpserver.AcceptTcpClient();
            WriteLine("Client connected....");


            NetworkStream networkStream = client.GetStream();

            byte[] buffer = new byte[1024];
            int readBytes;
            while ((readBytes = networkStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string data = Encoding.ASCII.GetString(buffer, 0, readBytes);
                WriteLine("GOT IT: {0}", data);

                byte[] response = Encoding.ASCII.GetBytes("I got your msg");
                networkStream.Write(response, 0, response.Length);
                WriteLine("Sent the server response");
            }

            client.Close();
        }
        catch (SocketException e)
        {
            WriteLine("SocketException: {0}", e);
        }
        finally
        {
            // Stop listening for connections
            tcpserver.Stop();
        }

        WriteLine("Server is stopped.");
        ReadLine();
    }
}

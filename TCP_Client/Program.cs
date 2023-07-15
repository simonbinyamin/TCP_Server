using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPClient
{
    static void Main()
    {
        try
        {

            IPAddress ipaddress = IPAddress.Parse("127.0.0.1");
            int port = 7070;
            TcpClient tcpclient = new TcpClient();
            tcpclient.Connect(ipaddress, port);
            tcpclient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);


            NetworkStream stream = tcpclient.GetStream();

            for (int i = 0; i < 5; i++)
            {

                string message = "Request nr " + (i + 1);
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);


                data = new byte[1024];
                int bytes = stream.Read(data, 0, data.Length);
                string response = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", response);
            }



            tcpclient.Close();
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }

        Console.ReadLine();


    }
}

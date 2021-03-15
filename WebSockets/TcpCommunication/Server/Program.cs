using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];

            TcpListener server = new TcpListener(ip, 8181);

            TcpClient client = default(TcpClient);

            try
            {
                server.Start();

                Console.WriteLine("Server started...");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.ReadLine();
            }

            while (true)
            {
                client = server.AcceptTcpClient();

                // the max length of an message is 1MB
                byte[] messageBuffer = new byte[1024];

                NetworkStream stream = client.GetStream();

                int messageLength = stream.Read(messageBuffer, 0, messageBuffer.Length);

                string message = Encoding.ASCII.GetString(messageBuffer, 0, messageLength);

                Console.WriteLine($"Byte {messageLength} {message}");
            }
        }
    }
}

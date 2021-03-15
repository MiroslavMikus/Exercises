using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client started...");

            while (true)
            {
                var text = Console.ReadLine();

                TcpClient client = new TcpClient("localhost", 8181);

                int byteCount = Encoding.ASCII.GetByteCount(text);

                byte[] dataToSend = new byte[byteCount];

                dataToSend = Encoding.ASCII.GetBytes(text);

                NetworkStream stream = client.GetStream();

                stream.Write(dataToSend, 0, dataToSend.Length);

                stream.Close();

                client.Close();
            }
        }
    }
}

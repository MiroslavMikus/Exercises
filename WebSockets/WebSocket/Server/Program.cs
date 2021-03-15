using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            WebsocketServer websocketServer = new WebsocketServer();

            websocketServer.Start("http://localhost:80/WebsocketHttpListenerDemo/");

            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }
    }
}

using System;
using System.Threading;

namespace Exercies.Stateless
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new Connection()
            {
                AutoReconnect = true
            };

            Console.WriteLine(connection.Graph);

            connection.Reconnect();

            Thread.Sleep(500);

            Console.WriteLine(connection.CurrentState);

            connection.SimulateDisconnect();

            Console.WriteLine(connection.CurrentState);

            Console.ReadLine();
        }
    }
}

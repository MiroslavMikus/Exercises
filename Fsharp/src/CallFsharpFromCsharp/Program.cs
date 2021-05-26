using System;
using API;
using UnitTest;

namespace CallFsharpFromCsharp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var connection = new Say.Connection() as IConnection;
            Console.WriteLine(connection.Message());
            Console.ReadLine();
            return 0;
        }
    }
}
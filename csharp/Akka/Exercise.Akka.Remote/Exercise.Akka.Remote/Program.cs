using Akka.Actor;
using Exercise_Akka.Common;
using System;
using System.Threading.Tasks;

namespace Exercise_Akka.Remote
{
    internal class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        private static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem in remote process");

            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            MovieStreamingActorSystem.WhenTerminated.Wait();

            Environment.Exit(1);
        }
    }
}

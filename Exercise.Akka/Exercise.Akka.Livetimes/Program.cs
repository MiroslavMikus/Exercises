using Akka.Actor;
using Exercise_Akka.Actors;
using Exercise_Akka.Messages;
using System;

namespace Exercise_Akka
{
    internal class Program
    {
        private static ActorSystem StreamingActorSystem;

        private static void Main(string[] args)
        {
            StreamingActorSystem = ActorSystem.Create("StreamingActorSystem");

            Console.WriteLine("Actor system was created");

            Props playbackActorProps = Props.Create<UserActor>();
            IActorRef actorRef = StreamingActorSystem.ActorOf(playbackActorProps, "UserActor");

            Console.ReadLine();
            ColorConsole.WriteWithColor("Sending play", ConsoleColor.Cyan);
            actorRef.Tell(new PlayMessage("Amazing movie", 42));

            Console.ReadLine();
            ColorConsole.WriteWithColor("Sending play", ConsoleColor.Cyan);
            actorRef.Tell(new PlayMessage("Play this", 3));

            Console.ReadLine();
            ColorConsole.WriteWithColor("Sending play", ConsoleColor.Cyan);
            actorRef.Tell(new StopMessage());

            Console.ReadLine();
            Console.WriteLine("Disposing actor system");
            StreamingActorSystem.Dispose();
            Console.WriteLine("Disposed");
            Console.ReadLine();
        }
    }
}

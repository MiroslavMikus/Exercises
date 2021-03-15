using System;
using System.Threading;
using Akka.Actor;
using Exercise_Akka.Common;
using Exercise_Akka.Common.Actors;
using Exercise_Akka.Common.Messages;

namespace Exercise_Akka.Client
{
    internal class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        private static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem");
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            ColorConsole.WriteLineGray("Creating actor supervisory hierarchy");
            MovieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            do
            {
                ShortPause();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteLineGray("enter a command and hit enter");

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command == "exit")
                {
                    MovieStreamingActorSystem.Terminate();
                    MovieStreamingActorSystem.WhenTerminated.Wait();
                    ColorConsole.WriteLineGray("Actor system shutdown");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

            } while (true);
        }

        // Perform a short pause for demo purposes to allow console to update nicely
        private static void ShortPause()
        {
            Thread.Sleep(450);
        }
    }
}

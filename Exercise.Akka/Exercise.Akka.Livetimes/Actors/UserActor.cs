using Akka.Actor;
using Exercise_Akka.Messages;
using System;

namespace Exercise_Akka.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _movie;
        public UserActor()
        {
            ColorConsole.WriteGreen("UserActor ctor");
            Stopped();
        }

        private void Playing()
        {
            Receive<StopMessage>(a => StopMovie());
            Receive<PlayMessage>(a => ColorConsole.WriteRed("Can't stop move!"));
            ColorConsole.WriteWithColor("User has become playing", ConsoleColor.Cyan);
        }

        private void Stopped()
        {
            Receive<PlayMessage>(a => StartMovie(a.MovieTitle));
            Receive<StopMessage>(a => ColorConsole.WriteRed("Can't start movie!"));
            ColorConsole.WriteWithColor("User has become Stopped", ConsoleColor.Cyan);
        }

        private void StopMovie()
        {
            ColorConsole.WriteYellow("Stopping " + _movie);

            _movie = string.Empty;

            Become(Stopped);
        }

        private void StartMovie(string movieTitle)
        {
            _movie = movieTitle;

            ColorConsole.WriteYellow("Watching " + _movie);

            Become(Playing);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteGreen("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreen("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreen("UserActor PreRestart, because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreen("UserActor PostRestart, because: " + reason);

            base.PostRestart(reason);
        }
    }
}

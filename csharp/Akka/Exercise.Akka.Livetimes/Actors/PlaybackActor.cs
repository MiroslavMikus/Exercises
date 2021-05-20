using Akka.Actor;
using Exercise_Akka.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_Akka.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Actor was created");

            Receive<PlayMessage>(a => OnReceive(a));
        }

        private void OnReceive(PlayMessage input)
        {
            ColorConsole.WriteYellow($"Received play message: [{input.MovieTitle}] [{input.UserId}]");
        }

        protected override void PreStart()
        {
            ColorConsole.WriteGreen("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreen("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreen("PlaybackActor PreRestart, because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreen("PlaybackActor PostRestart, because: " + reason);

            base.PostRestart(reason);
        }
    }
}

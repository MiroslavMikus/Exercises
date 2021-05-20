using System;
using Akka.Actor;
using Exercise_Akka.Hierarchy.Exceptions;

namespace Exercise_Akka.Hierarchy.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(ex => 
            {
                if (ex is SimulatedCorruptStateException)
                {
                    return Directive.Restart;
                }
                if (ex is SimulatedTerribleMovieException)
                {
                    return Directive.Resume;
                }
                return Directive.Restart;
            });
        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PreRestart because: {0}", reason.Message);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PostRestart because: {0} ", reason.Message);

            base.PostRestart(reason);
        }
        #endregion
    }
}
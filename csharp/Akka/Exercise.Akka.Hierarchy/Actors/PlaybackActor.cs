using System;
using Akka.Actor;
using Exercise_Akka.Hierarchy.Messages;

namespace Exercise_Akka.Hierarchy.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        private readonly IActorRef _userCoordinator;
        private readonly IActorRef _statistics;

        public PlaybackActor()
        {           
            _userCoordinator = Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            _statistics = Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");


            Receive<PlayMovieMessage>(message =>
                                      {
                                          ColorConsole.WriteGreen(
                                              "PlaybackActor received PlayMovieMessage '{0}' for user {1}",
                                              message.MovieTitle, message.UserId);

                                          _userCoordinator.Tell(message);                                         
                                      });
        }

        #region Lifecycle hooks
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
            ColorConsole.WriteGreen("PlaybackActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreen("PlaybackActor PostRestart because: " + reason);

            base.PostRestart(reason);
        } 
        #endregion
    }
}

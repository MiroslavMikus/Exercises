using Akka.Actor;
using Exercise_Akka.Hierarchy.Exceptions;
using Exercise_Akka.Hierarchy.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_Akka.Hierarchy.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts = new Dictionary<string, int>();

        public MoviePlayCounterActor()
        {
            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts[message.MovieTitle]++;
            }
            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }

            if (_moviePlayCounts[message.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            if (message.MovieTitle == "aha")
            {
                throw new SimulatedTerribleMovieException();
            }

            ColorConsole.WriteMagenta("MoviePlayCounterActor '{0}' has been watchend {1} times", message.MovieTitle, _moviePlayCounts[message.MovieTitle]);
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteGreen("MoviePlayCounterActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreen("MoviePlayCounterActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreen("MoviePlayCounterActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreen("MoviePlayCounterActor PostRestart because: " + reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}

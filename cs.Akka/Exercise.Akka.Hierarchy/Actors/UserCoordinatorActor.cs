using System;
using System.Collections.Generic;
using Akka.Actor;
using Exercise_Akka.Hierarchy.Messages;

namespace Exercise_Akka.Hierarchy.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {
                CreateChildUserIfNotExist(message.UserId);

                IActorRef childActor = _users[message.UserId];

                childActor.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildUserIfNotExist(message.UserId);

                IActorRef childActor = _users[message.UserId];

                childActor.Tell(message);
            });
        }

        private void CreateChildUserIfNotExist(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                var childActor = Context.ActorOf(Props.Create(() => new UserActor(userId)), "User" + userId);
                _users.Add(userId, childActor);

                ColorConsole.WriteCyan("UserCooridnator actor created new child UserActor for {0}, Total: {1}", userId, _users.Count);
            }
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteCyan("UserCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteCyan("UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteCyan("UserCoordinatorActor PreRestart because: {0}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteCyan("UserCoordinatorActor PostRestart because: {0}", reason);

            base.PostRestart(reason);
        } 
        #endregion
    }
}
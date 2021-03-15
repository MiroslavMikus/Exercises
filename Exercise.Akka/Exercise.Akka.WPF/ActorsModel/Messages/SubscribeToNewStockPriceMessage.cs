using Akka.Actor;

namespace Exercise_Akka.WPF.ActorsModel.Messages
{
    public class SubscribeToNewStockPriceMessage
    {
        public IActorRef Subscriber { get; }
        public SubscribeToNewStockPriceMessage(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }
    }
}

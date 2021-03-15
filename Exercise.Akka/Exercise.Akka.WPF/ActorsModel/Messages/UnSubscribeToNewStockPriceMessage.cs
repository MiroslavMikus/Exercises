using Akka.Actor;

namespace Exercise_Akka.WPF.ActorsModel.Messages
{
    public class UnSubscribeToNewStockPriceMessage
    {
        public IActorRef Subscriber { get; }
        public UnSubscribeToNewStockPriceMessage(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }
    }
}

using Akka.Actor;
using Exercise_Akka.WPF.ActorsModel.Messages;
using Exercise_Akka.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_Akka.WPF.ActorsModel.Actors.UI
{
    public class StockToogleButtonActor:ReceiveActor
    {
        private readonly IActorRef _coordinatorActor;
        private readonly string _stockSymbol;
        private readonly StockToogleButtonViewModel _viewModel;

        public StockToogleButtonActor(IActorRef stockCoordinatorActorRef, StockToogleButtonViewModel stockToogleViewModel, string stockSymbol)
        {
            _coordinatorActor = stockCoordinatorActorRef;
            _viewModel = stockToogleViewModel;
            _stockSymbol = stockSymbol;

            ToggledOff();
        }

        private void ToggledOff()
        {
            Receive<FlipToggleMessage>(message =>
            {
                _coordinatorActor.Tell(new WatchStockMessage(_stockSymbol));

                _viewModel.UpdateButtonTextToOn();

                Become(ToggledOn);
            });
        }

        private void ToggledOn()
        {
            Receive<FlipToggleMessage>(message =>
            {
                _coordinatorActor.Tell(new UnWatchStockMessage(_stockSymbol));

                _viewModel.UpdateButtonTextToOff();

                Become(ToggledOff);
            });
        }
    }
}

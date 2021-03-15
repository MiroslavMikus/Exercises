using Akka.Actor;
using Exercise_Akka.WPF.ActorsModel;
using Exercise_Akka.WPF.ActorsModel.Actors.UI;
using Exercise_Akka.WPF.ActorsModel.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Exercise_Akka.WPF.ViewModel
{
    public class StockToogleButtonViewModel : ViewModelBase
    {
        public string StockSymbol { get; }
        public ICommand ToggleCommand { get; }

        public IActorRef StockToggleButtonActorRef { get; }

        private string _buttonText;
        public string ButtonText
        {
            get { return _buttonText; }
            set { Set(ref _buttonText, value); }
        }

        public StockToogleButtonViewModel(IActorRef stockCoordinatorActorRef, string stockSymbol)
        {
            StockSymbol = stockSymbol;
            StockToggleButtonActorRef = ActorSystemReference
                .ActorSystem
                .ActorOf(Props.Create(() => new StockToogleButtonActor(stockCoordinatorActorRef, this, stockSymbol)));

            ToggleCommand = new RelayCommand(() =>
            {
                StockToggleButtonActorRef.Tell(new FlipToggleMessage());
            });

            UpdateButtonTextToOff();
        }

        public void UpdateButtonTextToOff()
        {
            ButtonText = ContructButtonText(false);
        }

        public void UpdateButtonTextToOn()
        {
            ButtonText = ContructButtonText(true);
        }

        private string ContructButtonText(bool isToggleOn)
        {
            return $"{StockSymbol} ({(isToggleOn ? "on" : "off")})";
        }
    }
}

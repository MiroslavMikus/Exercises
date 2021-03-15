using Akka.Actor;
using Exercise_Akka.WPF.ActorsModel;
using Exercise_Akka.WPF.ActorsModel.Actors;
using Exercise_Akka.WPF.ActorsModel.Actors.UI;
using GalaSoft.MvvmLight;
using OxyPlot;
using OxyPlot.Axes;
using System.Collections.Generic;

namespace Exercise_Akka.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IActorRef _chartingActorRef;
        private IActorRef _stocksCoordinatorActorRef;
        private PlotModel _plotModel;

        public Dictionary<string, StockToogleButtonViewModel> StockButtonViewModels { get; set; }

        public PlotModel PlotModel
        {
            get { return _plotModel; }
            set { Set(() => PlotModel, ref _plotModel, value); }
        }

        public MainViewModel()
        {
            SetUpChartModel();

            InitializeActors();

            CreateStockButtonViewModels();
        }

        private void SetUpChartModel()
        {
            _plotModel = new PlotModel
            {
                LegendTitle = "Legend",
                LegendOrientation = LegendOrientation.Horizontal,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.TopRight,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendBorder = OxyColors.Black
            };

            var stockDateTimeAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Date",
                StringFormat = "HH:mm:ss"
            };

            _plotModel.Axes.Add(stockDateTimeAxis);

            var stockPriceAxis = new LinearAxis
            {
                Minimum = 0,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Price"
            };

            _plotModel.Axes.Add(stockPriceAxis);
        }

        private void InitializeActors()
        {
            _chartingActorRef =
                ActorSystemReference.ActorSystem.ActorOf(Props.Create(() => new LineChartingActor(PlotModel)));

            _stocksCoordinatorActorRef =
                ActorSystemReference.ActorSystem.ActorOf(
                    Props.Create(() => new StockCoordinarotActor(
                        _chartingActorRef)), "StocksCoordinator");
        }

        private void CreateStockButtonViewModels()
        {
            StockButtonViewModels = new Dictionary<string, StockToogleButtonViewModel>();

            CreateStockButtonViewModel("AAAA");
            CreateStockButtonViewModel("BBBB");
            CreateStockButtonViewModel("CCCC");
        }

        private void CreateStockButtonViewModel(string stockSymbol)
        {
            var newViewModel = new StockToogleButtonViewModel(_stocksCoordinatorActorRef, stockSymbol);

            StockButtonViewModels.Add(stockSymbol, newViewModel);
        }
    }
}

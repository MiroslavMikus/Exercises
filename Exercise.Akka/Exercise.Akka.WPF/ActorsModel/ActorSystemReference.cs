using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Ninject;
using Exercise_Akka.WPF.ActorsModel.Actors;
using Exercise_Akka.WPF.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_Akka.WPF.ActorsModel
{
    static class ActorSystemReference
    {
        public static ActorSystem ActorSystem { get; private set; }

        static ActorSystemReference()
        {
            CreateActorSystem();
        }

        private static void CreateActorSystem()
        {
            ActorSystem = ActorSystem.Create("ReactiveStockActorSystem");

            var container = new StandardKernel();

            container.Bind<IStocPriceServiceGateway>().To<RandomStockPriceServiceGateway>();

            container.Bind<StockPriceLookugActor>().ToSelf();

            IDependencyResolver resolver = new NinjectDependencyResolver(container, ActorSystem);
        }
    }
}

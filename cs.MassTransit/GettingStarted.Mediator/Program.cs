using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Mediator;

namespace GettingStarted.Mediator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IMediator mediator = Bus.Factory.CreateMediator(cfg =>
            {
                cfg.Consumer<SubmitOrderConsumer>();
                cfg.Consumer<OrderStatusConsumer>();
            });

            Guid orderId = NewId.NextGuid();

            await mediator.Send<SubmitOrder>(new {OrderId = orderId});

            var client = mediator.CreateRequestClient<GetOrderStatus>();

            var response = await client.GetResponse<OrderStatus>(new {OrderId = orderId});

            Console.WriteLine("Order Status: {0}", response.Message.Status);
        }

        private static async Task SimpleMediator()
        {
            IMediator mediator = Bus.Factory.CreateMediator(cfg => { cfg.Consumer<TestConsumer>(); });

            await mediator.Publish(new Message {Text = "Hi"});

            Console.WriteLine("Press any key to exit");

            await Task.Run(Console.ReadKey);
        }
    }

    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            await Console.Out.WriteLineAsync($"Submit consumer received order: {context.Message.OrderId}");
        }
    }

    public interface GetOrderStatus
    {
        Guid OrderId { get; }
    }

    public interface OrderStatus
    {
        Guid OrderId { get; }
        string Status { get; }
    }

    class OrderStatusConsumer :
        IConsumer<GetOrderStatus>
    {
        public async Task Consume(ConsumeContext<GetOrderStatus> context)
        {
            await context.RespondAsync<OrderStatus>(new 
            { 
                context.Message.OrderId, 
                Status = "Pending" 
            });
        }
    }

    public class SubmitOrder
    {
        public Guid OrderId { get; init; }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;

namespace GettingStarted.Mediator
{
    public class TestConsumer : IConsumer<Message>
    {
        public async Task Consume(ConsumeContext<Message> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Text);
        }
    }
}
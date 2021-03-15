using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.PipeConfigurators;

namespace GettingStarted.Middleware
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingInMemory(sbc =>
            {
                sbc.UseExceptionLogger();

                sbc.ReceiveEndpoint("test_queue",
                    ep =>
                    {
                        ep.Handler<Message>(context => Console.Out.WriteLineAsync($"Received: {context.Message.Text}"));
                    });
            });

            await bus.StartAsync();

            await bus.Publish(new Message {Text = "Hi"});

            Console.WriteLine("Press any key to exit");

            await Task.Run(Console.ReadKey);

            await bus.StopAsync();
        }
    }

    public static class ExampleMiddlewareConfiguratorExtensions
    {
        public static void UseExceptionLogger(this IConsumePipeConfigurator config)
           
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var observer = new ExceptionLoggerObserver(config);
        }
    }

    public class ExceptionLoggerObserver : ConfigurationObserver, IMessageConfigurationObserver
    {
        public ExceptionLoggerObserver(IConsumePipeConfigurator configurator) : base(configurator)
        {
            Connect(this);
        }

        public void MessageConfigured<TMessage>(IConsumePipeConfigurator configurator) where TMessage : class
        {
            var specification = new ExceptionLoggerSpecification<TMessage>();
            configurator.AddPipeSpecification(specification);
        }
    }

    public class ExceptionLoggerSpecification<T> : IPipeSpecification<ConsumeContext<T>> where T : class
    {
        public void Apply(IPipeBuilder<ConsumeContext<T>> builder)
        {
            var filter = new ExceptionLoggerFilter<T>();
            builder.AddFilter(filter);
        }

        public IEnumerable<ValidationResult> Validate()
        {
            yield break;
        }
    }

    public class ExceptionLoggerFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            try
            {
                var payload = context.Message as Message;

                var t = context.GetType();
                
                Console.WriteLine($"Entering filter:{payload.Text}");

                await next.Send(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("error here");
            }
        }

        public void Probe(ProbeContext context)
        {
            var scope = context.CreateFilterScope("exceptionLogger");

            scope.Add("something", 1);
        }
    }

    public class Message
    {
        public string Text { get; init; }
    }
}
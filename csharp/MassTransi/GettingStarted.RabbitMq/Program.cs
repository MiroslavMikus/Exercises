using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MassTransit;

namespace GettingStarted.RabbitMq
{
    public static class Program
    {
        public static async Task Main()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost:5672/", config =>
                {
                    config.Username("rabbitmq");
                    config.Password("rabbitmq");
                    config.PublisherConfirmation = false;
                });

                sbc.ReceiveEndpoint("test_queue",
                    ep =>
                    {
                        ep.Handler<Message>(context =>
                            Task.CompletedTask); //return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    });
            });

            await bus.StartAsync();

            var publish = new List<Task>();

            var watch = new Stopwatch();

            watch.Start();

            var messagesCount = 100_000;
            for (int i = 0; i < messagesCount; i++)
            {
                publish.Add(bus.Publish(new Message {Text = $"Hi: {i}"}));
            }

            await Task.WhenAll(publish.ToArray());

            watch.Stop();

            double messeagesInSecond = messagesCount / ((double) watch.ElapsedMilliseconds / 1000);

            Console.WriteLine(messeagesInSecond + " m/s");
            Console.WriteLine($"Elapsed time {watch.ElapsedMilliseconds / 1000} s");

            Console.WriteLine("Press any key to exit");

            await Task.Run(Console.ReadKey);

            await bus.StopAsync();
        }
    }

    public class Message
    {
        public string Text { get; init; }
    }
}
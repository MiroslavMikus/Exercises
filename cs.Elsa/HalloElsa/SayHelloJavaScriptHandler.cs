using System;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Scripting.JavaScript.Messages;
using MediatR;

namespace HalloElsa
{
    public class SayHelloJavaScriptHandler : INotificationHandler<EvaluatingJavaScriptExpression>
    {
        public Task Handle(EvaluatingJavaScriptExpression notification, CancellationToken cancellationToken)
        {
            var engine = notification.Engine;
            engine.SetValue("sayHello", (Func<string, object>) (message => $"Hello {message}!"));
            return Task.CompletedTask;
        }
    }
}
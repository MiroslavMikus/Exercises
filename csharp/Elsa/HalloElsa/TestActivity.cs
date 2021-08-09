// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using Elsa.ActivityResults;
// using Elsa.Attributes;
// using Elsa.Expressions;
// using Elsa.Results;
// using Elsa.Services;
// using Elsa.Services.Models;
//
// namespace HalloElsa
// {
//     [ActivityDefinition(Category = "Custom", Description = "My first activity", Outcomes = new[] {"ok", "not_ok"}, DisplayName = "My test Activity")]
//     public class TestActivity : Activity
//     {
//         [ActivityProperty(Hint = "The message to write.")]
//         public WorkflowExpression<string> Message
//         {
//             get => GetState<WorkflowExpression<string>>();
//             set => SetState(value);
//         }
//
//         protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
//         {
//             var result = await context.EvaluateAsync(Message, cancellationToken);
//             
//             Console.WriteLine("##### Executing Test Activity");
//             
//             if (Message is null)
//             {
//                 return Outcome("not ok");
//             }            
//             
//             Console.WriteLine(result);
//             
//             return Outcome("ok");
//         }
//     }
// }
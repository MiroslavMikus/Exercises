using CommandLine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.CliParser
{
    partial class Program
    {
        static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<PrintOptions, CalcOptions>(args)
                .MapResult(
                  (PrintOptions o) =>
                  {
                      if (o.Revert)
                      {
                          o.Message = new string(o.Message.Reverse().ToArray());
                      }
                      Console.WriteLine(o.Message);

                      return 0;
                  },
                  (CalcOptions o) =>
                  {
                      switch (o.Operator)
                      {
                          case '+':
                              Console.WriteLine(o.Numbers.Sum());
                              break;
                          case '-':
                              Console.WriteLine(o.Numbers.Aggregate((a, b) => a - b));
                              break;
                          default:
                              Console.WriteLine("Operator is not supported!");
                              break;
                      }
                      return 0;
                  },
                  errs => 1);
        }
    }
}

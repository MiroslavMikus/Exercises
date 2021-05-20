using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace Exercise.CliParser
{
    partial class Program
    {
        [Verb("calc", HelpText = "Print text to the console interface.")]
        public class CalcOptions
        {
            [Option('o', "operator", Required = true, HelpText = "Math operator")]
            public char Operator { get; set; }

            [Option('n', "numbers", Required = false, HelpText = "Numbers to process")]
            public IEnumerable<int> Numbers { get; set; }

            [Usage(ApplicationAlias = "Cli.Exercise.exe")]
            public static IEnumerable<Example> Examples
            {
                get
                {
                    return new List<Example>() {
                        new Example("Add two 5 and 5 and 9", new CalcOptions { Operator = '+', Numbers = new int[] {5, 5, 9 } })
                    };
                }
            }
        }
    }
}

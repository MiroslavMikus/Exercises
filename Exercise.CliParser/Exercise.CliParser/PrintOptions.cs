using CommandLine;

namespace Exercise.CliParser
{
    partial class Program
    {
        [Verb("print", HelpText = "Print text to the console interface.")]
        public class PrintOptions
        {
            [Option('m', "message", Required = true, HelpText = "Text to print")]
            public string Message { get; set; }

            [Option('r', "revert", Required = false, HelpText = "Revert the text")]
            public bool Revert { get; set; }
        }
    }
}

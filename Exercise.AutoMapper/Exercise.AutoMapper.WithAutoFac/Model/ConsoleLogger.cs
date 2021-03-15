using System;

namespace Exercise.AutoMapper.WithAutoFac.Model
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string input)
        {
            Console.WriteLine(input);
        }
    }
}
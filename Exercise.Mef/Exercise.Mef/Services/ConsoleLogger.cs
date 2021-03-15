using Exercise.Mef.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Mef
{
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public class ConsoleLogger : ILogger
    {
        public void Log(string input)
        {
            Console.WriteLine(input);
        }
    }
}

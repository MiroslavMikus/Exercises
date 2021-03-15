using Exercise.Mef.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Mef
{
    public class InternalPlugin : IWorker
    {
        private readonly ConsoleLogger logger;

        [ImportingConstructor]
        public InternalPlugin(ConsoleLogger logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            logger.Log("Dispose internal plugin");
        }

        public string DoWork(string input)
        {
            logger.Log($"{nameof(InternalPlugin)} is using ConsoleLogger");
            return $"{nameof(InternalPlugin)} plugin at work: {input}";
        }
        
    }
}

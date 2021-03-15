using Exercise.Mef.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Mef.Plugin.SecondPlugin
{
    public class SecondPlugin : IWorker
    {
        [Import]
        private ICommunicationService _communicationService;

        public string DoWork(string input)
        {
            Console.WriteLine("Sending message from second plugin");

            _communicationService.Publish();

            return $"Second plugin at work: {input}";
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose second plugin");
        }
    }
}

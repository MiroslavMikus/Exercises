using Exercise.Mef.Api;
using System.ComponentModel.Composition;

namespace Exercise.Mef
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class Communication : ICommunicationService
    {
        [Import]
        private ILogger _logger;

        public void Publish()
        {
            _logger.Log("Sending message");
        }

        public void Subscribe()
        {
            _logger.Log("Receiving message");
        }
    }
}

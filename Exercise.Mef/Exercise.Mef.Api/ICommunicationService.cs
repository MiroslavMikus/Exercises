using System.ComponentModel.Composition;

namespace Exercise.Mef.Api
{
    [InheritedExport(typeof(ICommunicationService))]
    public interface ICommunicationService
    {
        void Publish();
        void Subscribe();
    }
}

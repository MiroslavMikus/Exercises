using System.ComponentModel.Composition;

namespace Exercise.Mef.Api
{
    [InheritedExport(typeof(ILogger))]
    public interface ILogger
    {
        void Log(string input);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Mef.Api
{
    [InheritedExport(typeof(IWorker))]
    public interface IWorker : IDisposable
    {
        string DoWork(string input);
    }
}

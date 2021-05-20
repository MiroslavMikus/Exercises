using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace Iot.GrainClasses.Interfaces
{
    public interface IDecodeGrain : IGrain, IGrainWithGuidKey
    {
        Task Decode(string message);
    }
}
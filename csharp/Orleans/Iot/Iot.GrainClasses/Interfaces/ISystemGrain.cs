using System.Threading.Tasks;
using Orleans;

namespace Iot.GrainClasses.Interfaces
{
    public interface ISystemGrain : IGrainWithStringKey
    {
        Task SetTemperaturature(double value, long deviceId);
        Task Subscribe(ISystemObserver observer);
        Task Unsubscribe(ISystemObserver observer);
    }
}
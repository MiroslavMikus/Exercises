using System.Threading.Tasks;
using Orleans;

namespace Iot.GrainClasses.Interfaces
{
    public interface IDeviceGrain : IGrainWithIntegerKey
    {
        Task SetTemperature(double value);
        Task JoinSystem(string name);
    }
}
using System.Security;
using System.Threading.Tasks;
using Iot.GrainClasses.Interfaces;
using Orleans;
using Orleans.Concurrency;

namespace Iot.GrainClasses.Implementation
{
    [StatelessWorker]
    public class DecodeGrain : Grain, IDecodeGrain
    {
        private readonly IGrainFactory _factory;

        public DecodeGrain(IGrainFactory factory)
        {
            _factory = factory;
        }

        public async Task Decode(string message)
        {
            var parts = message.Split((','));

            var grain = _factory.GetGrain<IDeviceGrain>(int.Parse(parts[0]));
            
            await grain.SetTemperature(double.Parse(parts[1]));
        }
    }
}
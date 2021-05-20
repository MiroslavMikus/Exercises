using System;
using System.Threading.Tasks;
using Iot.GrainClasses.Interfaces;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace Iot.GrainClasses.Implementation
{
    public class DeviceGrain : Grain, IDeviceGrain
    {
        private readonly IPersistentState<DeviceGrainState> _state;

        public DeviceGrain([PersistentState("device","sql")] IPersistentState<DeviceGrainState> state)
        {
            _state = state;
        }

        public override Task OnActivateAsync()
        {
            var id = this.GetPrimaryKeyLong();
            Console.WriteLine("Activated {0}", id);
            Console.WriteLine("Activated state: {0}", _state.State.LastValue);
            return base.OnActivateAsync();
        }

        public async Task SetTemperature(double value)
        {
            if (_state.State.LastValue < 100 && value >= 100)
            {
                Console.WriteLine("High temperature recorded {0}", value);
            }

            if (Math.Abs(_state.State.LastValue - value) > 0.1)
            {
                _state.State.LastValue = value;
                await _state.WriteStateAsync();
            }

            var systemGrain = GrainFactory.GetGrain<ISystemGrain>(_state.State.Name);
            await systemGrain.SetTemperaturature(value, this.GetPrimaryKeyLong());
        }

        public Task JoinSystem(string name)
        {
            _state.State.Name = name;
            return _state.WriteStateAsync();
        }
    }
}
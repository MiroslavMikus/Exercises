using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.GrainClasses.Interfaces;
using Orleans;

namespace Iot.GrainClasses.Implementation
{
    public class SystemGrain : Grain, ISystemGrain
    {
        private Dictionary<long, double> _temperatures;
        private HashSet<ISystemObserver> _observers;

        public override Task OnActivateAsync()
        {
            _temperatures = new Dictionary<long, double>();
            
            _observers = new HashSet<ISystemObserver>();
            
            RegisterTimer(this.Callback, null, TimeSpan.FromSeconds(5),TimeSpan.FromSeconds(5));
            
            return base.OnActivateAsync();
        }

        public Task SetTemperaturature(double value, long deviceId)
        {
            if (_temperatures.Keys.Contains(deviceId))
            {
                _temperatures[deviceId] = value;
            }
            else
            {
                _temperatures.Add(deviceId,value);
            }

            var average = _temperatures.Values.Average();
            
            Console.WriteLine($"System[{this.GetPrimaryKeyString()}] average temperature is [{average}]");
            
            return Task.CompletedTask;
        }

        public Task Subscribe(ISystemObserver observer)
        {
            _observers.Add(observer);
            return Task.CompletedTask;
        }

        public Task Unsubscribe(ISystemObserver observer)
        {
            _observers.Remove(observer);
            return Task.CompletedTask;
        }

        Task Callback(object callbackState)
        {
            if (!_temperatures.Values.Any())
            {
                return Task.CompletedTask;
            }
            
            var average = _temperatures.Values.Average();

            if (average > 100)
            {
                foreach (var observer in _observers)
                {
                    observer.HighTemperature(average);
                }       
            }

            return Task.CompletedTask;
        }
    }
}
using System;
using System.Collections.Generic;
using Iot.GrainClasses.Interfaces;
using Orleans;

namespace Iot.GrainClasses.Implementation
{
    public class SystemObserver : ISystemObserver
    {
        public void HighTemperature(double value)
        {
            Console.WriteLine($"System[{this.GetPrimaryKeyString()}] average temperature [{value}] is high!");
        }
    }
}
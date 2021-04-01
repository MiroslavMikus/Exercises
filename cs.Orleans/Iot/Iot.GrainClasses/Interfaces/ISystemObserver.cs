using System.Collections.Generic;
using Orleans;

namespace Iot.GrainClasses.Interfaces
{
    public interface ISystemObserver : IGrainObserver
    {
        void HighTemperature(double value);
    }
}
using System;

namespace Iot.GrainClasses.Implementation
{
    [Serializable]
    public class DeviceGrainState
    {
        public double LastValue { get; set; }
        public string Name { get; set; }
    }
}
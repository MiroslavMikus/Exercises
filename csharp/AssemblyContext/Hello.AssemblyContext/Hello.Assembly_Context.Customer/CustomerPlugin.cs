using System;

namespace Hello.Assembly_Context.Customer
{
    public class CustomerPlugin : IPlugin
    {
        public string Name { get; } = nameof(CustomerPlugin);
    }
}
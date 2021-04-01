using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Orleans.Concurrency;
using Xunit;

namespace Iot.Test
{
    public class TestImmutability
    {
        [Serializable, Immutable]
        public class OrleansMessage
        {
            public string Data { get; set; }

            public OrleansMessage(string data)
            {
                Data = data;
            }
        }
        
        [Fact]
        public void Test1()
        {
            var message = new OrleansMessage("someData");

            message.Data = "alsoData";
            message.Data.Should().Be("alsoData");

            message.Data = "aha";
            message.Data.Should().Be("aha");
        }
    }
}
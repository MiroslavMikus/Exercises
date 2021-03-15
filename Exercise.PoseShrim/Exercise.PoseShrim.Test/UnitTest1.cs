using System;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pose;

namespace Exercise.PoseShrim.Test
{
    /// <summary>
    /// https://github.com/tonerdo/pose
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Shim_static_method()
        {
            const string color = "Blue";

            Shim fileRead = Shim.Replace(() => File.ReadAllText(Is.A<string>())).With(delegate (string a) { return color; });

            PoseContext.Isolate(() =>
            {
                var car = new Car();

                car.Color.Should().Be(color);
            }, fileRead);
        }
    }
}

using FluentAssertions;
using Xunit;

namespace SomeCalculator.Test
{
    public class CalculatorTest
    {
        [Theory]
        [InlineData(1,2,3)]
        [InlineData(5,5,10)]
        [InlineData(8,-2,6)]
        public void Calculator_should_Add(int a, int b, int result)
        {

            Calculator.Add(a, b).Should().Be(result);
        }

        [Fact]
        public void Failing_test()
        {
            3.Should().NotBe(2);
        }
    }
}
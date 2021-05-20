using FluentAssertions;
using Optional.Unsafe;
using Xunit;

namespace Exercise.Option
{
    public class RepositoryOptionTest
    {
        [Fact]
        public void We_should_have_different_data()
        {
            var firstPerson = RepositoryOption.GetUser(2).ValueOrDefault();
            var secondPerson = RepositoryOption.GetUser(3).ValueOrDefault();
            firstPerson.Name.Should().NotBe(secondPerson.Name);
        }

        [Fact]
        public void We_should_have_only_100_users()
        {
            RepositoryOption.GetCar(100).HasValue.Should().BeFalse();
        }

        [Fact]
        public void We_should_have_only_100_cars()
        {
            RepositoryOption.GetCar(100).HasValue.Should().BeFalse();
        }
    }
}

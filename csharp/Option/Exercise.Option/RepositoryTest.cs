using FluentAssertions;
using Xunit;

namespace Exercise.Option
{
    public class RepositoryTest
    {
        [Fact]
        public void We_should_have_different_data()
        {
            Repository.GetUser(2).Name.Should().NotBe(Repository.GetUser(3).Name);
        }

        [Fact]
        public void We_should_have_only_100_users()
        {
            Repository.GetUser(100).Should().BeNull();
        }

        [Fact]
        public void We_should_have_only_100_cars()
        {
            Repository.GetCar(100).Should().BeNull();
        }
    }
}

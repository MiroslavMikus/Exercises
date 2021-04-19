using System.Threading.Tasks;
using Concept.DataStorage.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Concept.DataStorage.Tests
{
    [Collection("sqllite")]
    public class TestPerson
    {
        private readonly SqlLiteFixture _data;

        public TestPerson(SqlLiteFixture data)
        {
            _data = data;
        }

        [Fact]
        public async Task PersonShouldBeSaved()
        {
            _data.Context.Set<Person>().Add(new Person() {Name = "Miro"});
            await _data.Context.SaveChangesAsync();
            var miro = await _data.Context.Set<Person>().SingleAsync(a => a.Name == "Miro");
        }
    }

    [Collection("sqllite")]
    public class TestCar
    {
        private readonly SqlLiteFixture _data;

        public TestCar(SqlLiteFixture data)
        {
            _data = data;
        }

        [Fact]
        public async Task SaveCar()
        {
            _data.Context.Set<Car>().Add(new Car() {Color = "Red"});
            await _data.Context.SaveChangesAsync();
        }
    }
    
}
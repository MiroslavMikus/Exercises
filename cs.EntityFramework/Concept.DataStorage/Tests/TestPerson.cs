using System.Threading.Tasks;
using Concept.DataStorage.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Concept.DataStorage.Tests
{
    public class TestPerson : IClassFixture<SqlLiteFixture>
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
}
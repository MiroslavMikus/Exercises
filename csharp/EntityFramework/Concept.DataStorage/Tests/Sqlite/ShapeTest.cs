using System.Threading.Tasks;
using Concept.DataStorage.Model;
using Xunit;

namespace Concept.DataStorage.Tests
{
    [Collection("sqllite")]
    public class ShapeTest
    {
        private readonly SqlLiteFixture _data;

        public ShapeTest(SqlLiteFixture data)
        {
            _data = data;
        }

        [Fact]
        public async Task StoreMultipleShapes()
        {
            _data.Context.Set<User>().Add(new User()
            {
                Shape = new Shape()
                {
                    Length = "long"
                }
            });
            await _data.Context.SaveChangesAsync();
        }
    }
}
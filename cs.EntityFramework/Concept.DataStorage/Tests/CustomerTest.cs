using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concept.DataStorage.Model;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Concept.DataStorage.Tests
{
    [Collection("sqllite")]
    public class ShapteTest
    {
        private readonly SqlLiteFixture _data;

        public ShapteTest(SqlLiteFixture data)
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

    [Collection("sqllite")]
    public class CustomerTest
    {
        private readonly SqlLiteFixture _data;

        public CustomerTest(SqlLiteFixture data)
        {
            _data = data;
        }

        [Fact]
        public async Task StoreCustomer()
        {
            _data.Context.Set<Customer>().Add(new Customer()
            {
                Name = "Flokko",
                Orders = new List<Order>()
                {
                    new()
                    {
                        ItemName = "Some"
                    },
                    new()
                    {
                        ItemName = "Thing"
                    }
                }
            });
            await _data.Context.SaveChangesAsync();

            var customer = await _data.Context.Set<Customer>().SingleAsync(a => a.Name == "Flokko");

            customer.Orders.Any(a => a.ItemName == "Some").Should().BeTrue();
            customer.Orders.Any(a => a.ItemName == "Thing").Should().BeTrue();
        }

        [Fact]
        public async Task QueryOrders()
        {
            _data.Context.Set<Customer>().Add(new Customer()
            {
                Name = "someCust",
                Orders = new List<Order>()
                {
                    new()
                    {
                        ItemName = "Somer"
                    },
                    new()
                    {
                        ItemName = "Thingf"
                    }
                }
            });
            await _data.Context.SaveChangesAsync();

            _data.Context.Set<Order>().Any(a => a.ItemName == "Somer").Should().BeTrue();
            _data.Context.Set<Order>().Any(a => a.ItemName == "Thingf").Should().BeTrue();
        }
    }
}
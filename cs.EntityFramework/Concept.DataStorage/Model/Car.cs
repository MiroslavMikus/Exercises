using Concept.DataStorage.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concept.DataStorage.Model
{
    [Persistent]
    public sealed class Car
    {
        public int CarKey { get; set; }
        public string Color { get; set; }
    }


    public class BaseEntityConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(obj => obj.CarKey);
        }
    }
}
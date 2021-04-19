using System;
using System.Linq;
using Concept.DataStorage.Model;
using Microsoft.EntityFrameworkCore;

namespace Concept.DataStorage.Context
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new Type[0]);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var entityTypes = assembly
                    .GetTypes()
                    .Where(t =>
                        t.GetCustomAttributes(typeof(PersistentAttribute), inherit: true)
                            .Any()).ToList();

                modelBuilder.Entity<Person>();

                foreach (var type in entityTypes)
                {
                    entityMethod.MakeGenericMethod(type)
                        .Invoke(modelBuilder, new object[] { });
                }
            }
        }
    }
}
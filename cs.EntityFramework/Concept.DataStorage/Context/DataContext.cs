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
            modelBuilder.HasDefaultSchema("data");
            
            var addMethod = typeof(ModelBuilder).GetMethod("ApplyConfiguration");

            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new Type[0]);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var entityTypes = assembly
                    .GetTypes()
                    .Where(t =>
                        t.GetCustomAttributes(typeof(PersistentAttribute), inherit: true)
                            .Any()).ToList();

                if (!entityTypes.Any()) continue;

                var configTypes = assembly
                    .GetTypes()
                    .Where(t => t.BaseType != null
                                && t.BaseType.IsGenericType
                                && t.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

                var configTypesLookup =
                    configTypes.ToDictionary(a => a.BaseType.GetGenericArguments().Single(), b => b);

                foreach (var type in entityTypes)
                {
                    if (configTypesLookup.ContainsKey(type))
                    {
                        addMethod.MakeGenericMethod(type)
                            .Invoke(modelBuilder, new object[] {configTypesLookup[type]});
                    }
                    else
                    {
                        entityMethod.MakeGenericMethod(type)
                            .Invoke(modelBuilder, new object[] { });
                    }
                }
            }
        }
    }
}
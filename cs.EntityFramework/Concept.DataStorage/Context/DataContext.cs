using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Concept.DataStorage.Context
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        private Type ImplementsGenericInterface(Type candidate, Type interfaceType)
        {
            return candidate.GetInterfaces()
                .FirstOrDefault(a => a.IsGenericType && a.GetGenericTypeDefinition() == interfaceType)
                ?.GetGenericArguments()[0];
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
                    .Select(t => new
                        {Key = ImplementsGenericInterface(t, typeof(IEntityTypeConfiguration<>)), Config = t})
                    .Where(a => a.Key != null).ToArray();

                foreach (var type in entityTypes)
                {
                    var config = configTypes.FirstOrDefault(a => a.Key == type);
                    if (config is not null)
                    {
                        addMethod.MakeGenericMethod(type)
                            .Invoke(modelBuilder, new object[] {Activator.CreateInstance(config.Config) });
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
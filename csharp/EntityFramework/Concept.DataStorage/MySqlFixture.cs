using System;
using System.Threading.Tasks;
using Concept.DataStorage.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Concept.DataStorage
{
    [CollectionDefinition("mysql")]
    public class MySqlCollection : ICollectionFixture<MySqlFixture>
    {
    }
    
    public class MySqlFixture : IAsyncLifetime
    {
        public DataContext Context { get; set; }

        private const string Db = "server=localhost;port=3306;database=testdb;uid=root;password=example";

        public async Task InitializeAsync()
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

            var opt = new DbContextOptionsBuilder()
                .UseMySql(Db, serverVersion)
                .EnableSensitiveDataLogging() // These two calls are optional but help
                .EnableDetailedErrors()
                .Options;

            Context = new DataContext(opt);

            await Context.Database.MigrateAsync();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
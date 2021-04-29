using System;
using System.Threading.Tasks;
using Concept.DataStorage.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Concept.DataStorage
{
    [CollectionDefinition("mssql")]
    public class MsSqlCollection : ICollectionFixture<MsSqlFixture>
    {
    }
    
    public class MsSqlFixture : IAsyncLifetime
    {
        public DataContext Context { get; set; }

        private const string Db = "server=localhost;database=testdb;uid=sa;pwd=Example123";

        public async Task InitializeAsync()
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

            var opt = new DbContextOptionsBuilder()
                .UseSqlServer(Db)
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
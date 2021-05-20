using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Concept.DataStorage.Context;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Xunit;

namespace Concept.DataStorage
{
    [CollectionDefinition("sqllite")]
    public class SqlLiteCollection : ICollectionFixture<SqlLiteFixture>
    {
    }

    public class SqlLiteFixture : IAsyncLifetime
    {
        public DataContext Context { get; set; }

        private const string Db = "test.sqlite";

        public async Task InitializeAsync()
        {
            var opt = new DbContextOptionsBuilder().UseSqlite($"Data Source={Db}").Options;

            Context = new DataContext(opt);

            await Context.Database.MigrateAsync();
        }

        public Task DisposeAsync()
        {
            File.Delete(Db);
            return Task.CompletedTask;
        }
    }
}
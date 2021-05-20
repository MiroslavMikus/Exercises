using FluentAssertions;
using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.LiteDb.Test.Filter
{
    [TestClass]
    public class FilterTests
    {
        private const string dbName = "CustomerTest.db";

        [ClassInitialize]
        public static void Startup()
        {
            var customers = Enumerable.Range(1, 200).Select(a => new Customer
            {
                Name = Guid.NewGuid().ToString(),
                Birthday = DateTime.Now - TimeSpan.FromDays(a)
            }).ToList();

            customers.Add(new Customer { Birthday = new DateTime(2020, 2, 22), Name = "SuperCustomer", IsNotActive = false });
            customers.Add(new Customer { Birthday = new DateTime(2019, 2, 22), Name = "Customer", IsNotActive = false });
            customers.Add(new Customer { Birthday = new DateTime(2018, 2, 22), Name = "AlsoCustomer", IsNotActive = false });

            using var db = new LiteDatabase(dbName);

            var repo = db.GetCollection<Customer>("customers");

            repo.InsertBulk(customers);
        }

        [ClassInitialize]
        public static void Cleanup()
        {
            File.Delete(dbName);
        }

        [TestMethod]
        public void FilterTest()
        {
            using var db = new LiteDatabase(dbName);

            var repo = db.GetCollection<Customer>("customers");

            // Typesafe search
            var customer1 = repo.Find(a => a.Name == "Customer").FirstOrDefault();

            // Dynamic search
            var query = Query.EQ("Name", new BsonValue("Customer"));
            
            var customer2 = repo.Find(query).FirstOrDefault();

            customer2.Id.Should().Be(customer1.Id);

            var bsonRepo = db.GetCollection("customers");

            var customer3 = bsonRepo.Find(query).FirstOrDefault();

            var testmp = customer3["Name"];

            customer3["_id"].AsInt32.Should().Be(customer1.Id);
        }
    }
}

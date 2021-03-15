using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Exercise.LiteDb.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ExampleInsert()
        {
            using (var db = new LiteDatabase(@"FirstDatabase.db"))
            {
                // Get customer collection
                var customers = db.GetCollection<Customer>("customers");

                // Create your new customer instance
                var customer = new Customer
                {
                    Name = "Miroslav Mikus",
                    Phones = new string[] { "8000-0000", "9000-0000" },
                    //IsActive = true
                };

                // Insert new customer document (Id will be auto-incremented)
                customers.Insert(customer);

                // Update a document inside a collection
                customer.Name = "Joana Doe";

                customers.Update(customer);

                // Index document using a document property
                customers.EnsureIndex(x => x.Name);

                //Use Linq to query documents
                var results = customers.Find(x => x.Name.StartsWith("Jo"));
            }
        }

        [TestMethod]
        public void ExampleUpdate()
        {
            using (var db = new LiteDatabase(@"FirstDatabase.db"))
            {
                var customer = db.GetCollection<Customer>("customers");

                var joanna = customer.Find(a => a.Id == 1).FirstOrDefault();

                joanna.IsNotActive = true;

                customer.Update(joanna);
            }

            using (var db = new LiteDatabase(@"FirstDatabase.db"))
            {
                var customer = db.GetCollection<Customer>("customers");

                var joanna = customer.Find(a => a.Id == 1).FirstOrDefault();
            }
        }

        [TestMethod]
        [TestCategory("Performance")]
        public void Add1000Customers()
        {
            var dbName = Guid.NewGuid().ToString() + ".db";

            using (var db = new LiteDatabase(dbName))
            {
                var customers = Enumerable.Range(1, 10000).Select(a => new Customer
                {
                    Name = Guid.NewGuid().ToString(),
                    Birthday = DateTime.Now - TimeSpan.FromDays(a)
                });

                var repo = db.GetCollection<Customer>("customers");

                foreach (var cus in customers)
                {
                    repo.Insert(cus);
                }
            }
            File.Delete(dbName);
        }

        [TestMethod]
        [TestCategory("Performance")]
        public void Add1000CustomersCollection()
        {
            var dbName = Guid.NewGuid().ToString() + ".db";

            using (var db = new LiteDatabase(dbName))
            {
                var customers = Enumerable.Range(1, 10000).Select(a => new Customer
                {
                    Name = Guid.NewGuid().ToString(),
                    Birthday = DateTime.Now - TimeSpan.FromDays(a)
                });

                var repo = db.GetCollection<Customer>("customers");

                repo.Insert(customers);
            }

            File.Delete(dbName);
        }

        [TestMethod]
        [TestCategory("Performance")]
        public void Add1000CustomersBulk()
        {
            var dbName = Guid.NewGuid().ToString() + ".db";

            using (var db = new LiteDatabase(dbName))
            {
                var customers = Enumerable.Range(1, 10000).Select(a => new Customer
                {
                    Name = Guid.NewGuid().ToString(),
                    Birthday = DateTime.Now - TimeSpan.FromDays(a)
                });

                var repo = db.GetCollection<Customer>("customers");

                repo.InsertBulk(customers);
            }

            File.Delete(dbName);
        }
    }
}

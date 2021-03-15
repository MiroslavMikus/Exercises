using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Exercise.LiteDb.Test.StoreComplexObjectExample
{
    [TestClass]
    public class ComplexStoreTest
    {
        private const string dbName = @"SecondDatabase.db";

        [TestMethod]
        public void TestStore()
        {
            using (var db = new LiteDatabase(dbName))
            {
                var customer = db.GetCollection<ComplexCustomer>("customers");

                customer.Insert(new ComplexCustomer()
                {
                    Birthday = DateTime.Now,
                    IsNotActive = true,
                    Name = "Miro",
                    Address = new Address
                    {
                        City = "Oberaudorf",
                        Street = "Bahnhof"
                    }
                });
            }
        }

        [TestMethod]
        public void TestRepository()
        {
            using (var repo = new LiteRepository(dbName))
            {
                repo.Insert(new ComplexCustomer()
                {
                    Birthday = DateTime.Now,
                    IsNotActive = true,
                    Name = "Miroslav",
                    Address = new Address
                    {
                        City = "Oberaudorf",
                        Street = "Bahnhof"
                    }
                }, "customers");
            }
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            File.Delete(dbName);
        }
    }
}

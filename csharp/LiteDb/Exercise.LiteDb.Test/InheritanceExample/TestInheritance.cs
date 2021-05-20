using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Exercise.LiteDb.Test.InheritanceExample
{
    [TestClass]
    public class TestInheritance
    {
        [TestMethod]
        public void InheritanceTest()
        {
            string dbName = "FourthDatabase.db";

            using (var db = new LiteDatabase(dbName))
            {
                var customers = db.GetCollection<Customer>("customers");

                var customer = new Customer
                {
                    CustomerNr = 12,
                    Name = "Miro"
                };

                customers.Insert(customer);
            }

            using (var db = new LiteDatabase(dbName))
            {
                var persons = db.GetCollection<Person>("customers");

                var customer = persons.Find(a => a.Name == "Miro");

                Assert.IsNotNull(customer);
            }

            File.Delete(dbName);
        }
    }
}

using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Exercise.LiteDb.Test.ReferenceDocumentsExample
{
    [TestClass]
    public class ReferenceDocumentTest
    {
        //public ReferenceDocumentTest()
        //{
        //    BsonMapper.Global
        //        .Entity<Order>()
        //        .DbRef(x => x.Customer, "customers");
        //}

        [TestMethod]
        public void TestStoreReferences()
        {
            using (var db = new LiteRepository("ThirdDatabase.db"))
            {
                var customer = new Customer()
                {
                    Name = "Miro"
                };

                var order = new Order
                {
                    Customer = customer
                };

                db.Insert(customer, "customers");

                db.Insert(order, "orders");
            }

            using (var db = new LiteRepository("ThirdDatabase.db"))
            {
                var order = db.Query<Order>("orders")
                    .Include(a => a.Customer)
                    .FirstOrDefault();

                Assert.IsNotNull(order.Customer);

                Assert.AreEqual("Miro",order.Customer.Name);
            }

            File.Delete("ThirdDatabase.db");
        }
    }
}

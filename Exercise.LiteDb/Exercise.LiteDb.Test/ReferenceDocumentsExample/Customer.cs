using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise.LiteDb.Test.ReferenceDocumentsExample
{
    public class Customer
    {
        [BsonId]
        public int CustomerId { get; set; }
        public string Name { get; set; }
    }

}

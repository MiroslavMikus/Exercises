using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise.LiteDb.Test.StoreComplexObjectExample
{
    class ComplexCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Phones { get; set; }
        public bool IsNotActive { get; set; }

        public DateTime Birthday { get; set; }
        public Address Address { get; set; }
    }

    class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
}

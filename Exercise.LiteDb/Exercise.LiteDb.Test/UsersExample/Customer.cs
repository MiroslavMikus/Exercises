using System;

namespace Exercise.LiteDb.Test
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Phones { get; set; }
        public bool IsNotActive { get; set; }

        public DateTime Birthday { get; set; }
    }

}

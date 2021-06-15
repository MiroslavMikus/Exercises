using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Concept.DataStorage.Context;

namespace Concept.DataStorage.Model
{
    [Persistent]
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public string ItemColor { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
    }
}
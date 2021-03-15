using LiteDB;

namespace Exercise.LiteDb.Test.ReferenceDocumentsExample
{
    public class Order
    {
        [BsonId]
        public int OrderId { get; set; }

        [BsonRef("customers")]
        public Customer Customer { get; set; }
    }

}

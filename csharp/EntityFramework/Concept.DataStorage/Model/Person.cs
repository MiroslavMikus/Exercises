using System.ComponentModel.DataAnnotations.Schema;
using Concept.DataStorage.Context;

namespace Concept.DataStorage.Model
{
    [Persistent]
    [Table("SomePerson")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
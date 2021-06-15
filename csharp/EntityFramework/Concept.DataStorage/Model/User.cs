
using Concept.DataStorage.Context;

namespace Concept.DataStorage.Model
{
    [Persistent]
    public sealed class User
    {
        public int Id { get; set; }
        public Shape Shape { get; set; }
    }

    [Persistent]
    public class Shape
    {
        public int Id { get; set; }
        public string Length { get; set; }
    }

    [Persistent]
    public class Pc
    {
        public int Id { get; set; }
        public Shape Shape { get; set; }
    }
}
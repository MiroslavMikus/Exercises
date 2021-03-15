using System.Collections.Generic;

namespace Exercise.AutoMapper.Model
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>
        {
            new Car {Color = "red", Brand = "Opel", Age ="5"},
            new Car {Color = "blue", Brand = "BMW", Age ="6"},
            new Car {Color = "green", Brand = "Audi", Age ="7"}
        };

        public Address PrivateAddress { get; set; } = new Address
        {
            City = "Rosenheim",
            PostCode = "83022"
        };
    }
}

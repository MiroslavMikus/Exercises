using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Exercise.AutoMapper.Model
{
    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt{ get; set; }
        public IEnumerable<WorkCar> Cars { get; set; }
        public string Address { get; set; }
    }
}

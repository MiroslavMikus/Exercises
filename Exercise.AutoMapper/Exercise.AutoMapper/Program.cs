using AutoMapper;
using Exercise.AutoMapper.Model;
using Exercise.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.AutoMapper
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person
            {
                Name = "Miroslav",
                Age = 18
            };

            // Create map
            Mapper.Initialize(cfg => cfg.AddProfile<TestProfile>());

            Employee emp = Mapper.Map<Employee>(p);

            Console.WriteLine(emp.Name);
            Console.WriteLine(emp.Age);
            Console.WriteLine(emp.CreatedAt);
            Console.WriteLine(emp.Address);

            Console.ReadLine();
        }
    }
}

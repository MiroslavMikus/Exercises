using AutoMapper;
using Exercise.AutoMapper.Composite.Auto_Mapper;
using Exercise.AutoMapper.Composite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.AutoMapper.Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationProvider provider = new MapperConfiguration(conf => conf.AddProfile(new MapperProfile()));

            IMapper mapper = provider.CreateMapper();

            IEnumerable<UserSource> user = FakeStorage.GetUsers();

            IList<UserTarget> usertarget = mapper.Map<IList<UserTarget>>(user);

            foreach (var item in usertarget)
            {
                Console.WriteLine($"element nr.: {usertarget.IndexOf(item)}");
                Console.WriteLine(item.Print());
            }

            Console.ReadLine();
        }
    }
}

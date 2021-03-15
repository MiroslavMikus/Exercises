using Autofac;
using AutoMapper;
using Exercise.AutoMapper.WithAutoFac.Auto_fac;
using Exercise.AutoMapper.WithAutoFac.Auto_Mapper;
using Exercise.AutoMapper.WithAutoFac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.AutoMapper.WithAutoFac
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutoFacModule());

            var scope = builder.Build().BeginLifetimeScope();

            IConfigurationProvider provider = new MapperConfiguration(conf => conf.AddProfile(new AutoMapperProfile(scope)));

            IMapper mapper = provider.CreateMapper();

            // Init static mapper ->
            // Mapper.Initialize(conf => conf.AddProfile(new AutoMapperProfile(scope)));

            User user = mapper.Map<User>(new UserData());

            user.WriteName();

            Console.ReadLine();
        }
    }
}

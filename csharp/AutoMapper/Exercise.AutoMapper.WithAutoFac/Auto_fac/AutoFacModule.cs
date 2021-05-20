using Autofac;
using Exercise.AutoMapper.WithAutoFac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.AutoMapper.WithAutoFac.Auto_fac
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<User>().AsSelf();

            builder.RegisterType<ConsoleLogger>()
                        .As<ILogger>()
                        .SingleInstance();
        }
    }
}

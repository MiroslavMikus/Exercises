using Autofac;
using AutoMapper;
using Exercise.AutoMapper.WithAutoFac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.AutoMapper.WithAutoFac.Auto_Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(ILifetimeScope scope)
        {
            CreateMap<UserData, User>().ConstructUsing(x => scope.Resolve<User>());
        }
    }
}

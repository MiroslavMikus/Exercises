using AutoMapper;
using Exercise.AutoMapper.Composite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.AutoMapper.Composite.Auto_Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserSource, UserTarget>();
        }
    }
}

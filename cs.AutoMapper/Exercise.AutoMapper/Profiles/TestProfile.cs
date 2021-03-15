using AutoMapper;
using Exercise.AutoMapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.AutoMapper.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            this.CreateMap<Person, Employee>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.PrivateAddress.City}-{src.PrivateAddress.PostCode}"))
                .AfterMap((src,dest) => dest.CreatedAt = DateTime.Now);

            this.CreateMap<Car, WorkCar>();
        }
    }
}

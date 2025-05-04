using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucky.Ecommerce.Application.Dto;
using Lucky.Ecommerce.Domain.Entity;
using AutoMapper;

namespace Lucky.Ecommerce.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Products, ProductsDto>().ReverseMap();
            CreateMap<Users, UsersDto>().ReverseMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.models;
using AutoMapper;

namespace asp_net_ecommerce_web_api.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile(){
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
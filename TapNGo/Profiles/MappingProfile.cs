using AutoMapper;
using TapNGo.DAL.Models;
using TapNGo.DTOs;

namespace TapNGo.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuCategory, CategoryResponseDto>().ReverseMap();
            CreateMap<MenuCategory, CategoryCreateDto>().ReverseMap();
            CreateMap<MenuCategory, CategoryUpdateDto>().ReverseMap();
        }
    }
}

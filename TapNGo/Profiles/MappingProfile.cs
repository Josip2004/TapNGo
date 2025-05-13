using AutoMapper;
using TapNGo.DTOs;
using TapNGo.Models;

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

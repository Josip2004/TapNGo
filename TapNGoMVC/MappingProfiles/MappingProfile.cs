using AutoMapper;
using TapNGo.DAL.Models;
using TapNGo.DTOs;
using TapNGoMVC.ViewModels;

namespace TapNGoMVC.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItem, MenuVM>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.MenuCategory.Name))
                .ForMember(dest => dest.Quantity, opt => opt.Ignore());


            CreateMap<User, UserRegisterVM>().ReverseMap();
            CreateMap<User, UserLoginVM>().ReverseMap();
        }
    }
}

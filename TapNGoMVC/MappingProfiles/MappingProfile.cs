using AutoMapper;
using TapNGo.Models;
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
        }
    }
}

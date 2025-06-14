using AutoMapper;
using TapNGo.DAL.Models;
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

            CreateMap<MenuVM, MenuItem>()
                 .ForMember(dest => dest.MenuCategory, opt => opt.Ignore());

            CreateMap<MenuItem, MenuEditVM>()
                .ForMember(dest => dest.MenuCategoryId, opt => opt.MapFrom(src => src.MenuCategoryId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<MenuEditVM, MenuItem>()
                .ForMember(dest => dest.MenuCategoryId, opt => opt.MapFrom(src => src.MenuCategoryId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MenuCategory, opt => opt.Ignore());


            CreateMap<User, UserRegisterVM>().ReverseMap();
            CreateMap<User, UserLoginVM>().ReverseMap();

            CreateMap<MenuCategory, MenuCategoryVM>();
            CreateMap<MenuCategoryVM, MenuCategory>();

            CreateMap<Order, OrderVM>().ReverseMap();

            CreateMap<OrderItem, OrderItemVM>()
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name));

            CreateMap<ReviewVM, Review>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<Review, ReviewVM>();
             

        }
    }
}

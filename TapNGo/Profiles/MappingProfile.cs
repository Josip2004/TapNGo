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

            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();

            CreateMap<MenuItem, MenuItemResponseDTO>().ReverseMap();
            CreateMap<MenuItem, MenuItemCreateDTO>().ReverseMap();
            CreateMap<MenuItem, MenuItemUpdateDTO>().ReverseMap();

            CreateMap<Review, ReviewResponseDTO>().ReverseMap();
            CreateMap<Review, ReviewCreateDTO>().ReverseMap();

            CreateMap<Order, OrderResponseDTO>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

            CreateMap<OrderItem, OrderItemDetailDTO>()
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name))
                .ForMember(dest => dest.PricePerItem, opt => opt.MapFrom(src => src.MenuItem.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity ?? 0));

        }
    }
}

using AutoMapper;
using Restaurant.BLL.Dtos.MenuItemDtos;
using Restaurant.BLL.Dtos.OrderDtos;
using Restaurant.BLL.Dtos.OrderItemDtos;
using Restaurant.Core.Models;
using Restaurant.DAL.Models;

namespace Restaurant.BLL.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<MenuItemCreateDto, MenuItem>();
            CreateMap<MenuItem, MenuItemReturnDto>();

            CreateMap<OrderItem, OrderItemReturnDto>()
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MenuItem.Price))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));

            CreateMap<Order, OrderReturnDto>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        }
    }
}

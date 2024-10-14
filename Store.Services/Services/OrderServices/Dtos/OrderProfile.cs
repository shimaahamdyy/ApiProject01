using AutoMapper;
using Store.Data.Entities.OrderEntities;
using Store.Services.Services.ProductServices;

namespace Store.Services.Services.OrderServices.Dtos
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<ShippingAddress , AddressDto>().ReverseMap();

            CreateMap<Order , OrderDetailsDto>()
                .ForMember(dest => dest.DeliveryMethodName, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.ProductItem.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.ProductItem.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>()).ReverseMap();

        }
    }
}

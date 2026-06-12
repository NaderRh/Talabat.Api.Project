using AutoMapper;
using Talabat.Core.Dto.Basket_Dto;
using Talabat.Core.Dto.Order_Dto;
using Talabat.Core.Dto.User_Dto;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Basket_Module;
using Talabat.Core.Entities.Order_Module;
using Talabat.Core.Entities.Product_Module;
using Talabat.Core.Entities.Security_Module;
using Talabat.Core.Repositories.Dto;
using orderAddress = Talabat.Core.Entities.Order_Module.Address;
using userAddress = Talabat.Core.Entities.Security_Module.Address;
using userAddressDto = Talabat.Core.Dto.User_Dto.UserAddressDto;
using orderAddressDto = Talabat.Core.Dto.Order_Dto.AddressDto;



namespace API.Talabat.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>().ForMember(s => s.Brand, o => o.MapFrom(d => d.ProductBrand.Name))
                                                    .ForMember(s=>s.Category,o=>o.MapFrom(d=>d.ProductCategory.Name))
                                                    .ForMember(s=>s.PictureUrl,o=>o.MapFrom<ResolvePictureUrlDto>());
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItems, BasketItemsDto>().ReverseMap();
            CreateMap<orderAddressDto, orderAddress>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(P => P.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(P => P.DeliverMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(P => P.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(P => P.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(P => P.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<userAddressDto, userAddress>().ReverseMap();
        }
    }
}

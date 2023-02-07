using API.Dtos;
using AutoMapper;
using Core.Models;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(destination => destination.ProductType, o => o.MapFrom(s => s.ProductType.Kind))
            .ForMember(destination => destination.ProductStock, o => o.MapFrom(s => s.ProductStock.ProductsLeft))
            .ForMember(destination=> destination.ProductImages, o=>o.MapFrom(s=>s.ProductImages.Select(x=>x.Url)))
            .ForMember(d => d.MainImageUrl, o => o.MapFrom<ProductUrlResolver>())
            .ForMember(d => d.ProductImages, o => o.MapFrom<ProductPicturesUrlResolver>())
            ;
        
        CreateMap<Core.Models.Identity.UserAddress, UserAddressDto>().ReverseMap();
        CreateMap<CustomerBasketDto, CustomerBasket>();
        CreateMap<BasketItemDto, BasketItem>();
    }
}
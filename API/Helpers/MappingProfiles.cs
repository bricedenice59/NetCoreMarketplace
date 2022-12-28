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
            ;
    }
}
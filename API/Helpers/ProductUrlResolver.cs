using API.Dtos;
using AutoMapper;
using Core.Models;

namespace API.Helpers;

public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
{
    private readonly IConfiguration _config;
    public ProductUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
    {
        destination.ProductImages = source.ProductImages.Where(x => !string.IsNullOrEmpty(x.Url)).Select(s => _config["ApiUrl"] + s.Url).ToList();
        
        if (!string.IsNullOrEmpty(source.MainImageUrl)) 
        {
            return _config["ApiUrl"] + source.MainImageUrl;
        }
        return null;
    }   
}
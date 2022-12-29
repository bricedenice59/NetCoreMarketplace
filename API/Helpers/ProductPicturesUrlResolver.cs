using API.Dtos;
using AutoMapper;
using Core.Models;

namespace API.Helpers;

public class ProductPicturesUrlResolver : IValueResolver<Product, ProductDto, ICollection<string>>
{
    private readonly IConfiguration _config;
    public ProductPicturesUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public ICollection<string> Resolve(Product source, ProductDto destination, ICollection<string> destMember, ResolutionContext context)
    {
        return source.ProductImages?
            .Where(x => !string.IsNullOrEmpty(x.Url))
            .Select(s => _config["ApiUrl"] + s.Url)
            .ToList();
    }   
}
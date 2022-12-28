using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Product : BaseModel
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public double PriceWithExcludedVAT { get; set; }
    
    [Required]
    public string MainImageUrl { get; set; }

    [ForeignKey("ProductStockId")]
    public Guid ProductStockId { get; set; }
    public ProductStock ProductStock { get; set; }
    
    [ForeignKey("ProductTypeId")]
    public Guid ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; }
}
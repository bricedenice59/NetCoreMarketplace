using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Product : BaseModel
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }

    [ForeignKey("ProductStockId")]
    public Guid ProductStockId { get; set; }
    public ProductStock ProductStock { get; set; }
    
    [ForeignKey("ProductTypeId")]
    public Guid ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    
    [ForeignKey("ProductPriceId")]
    public Guid ProductPriceId { get; set; }
    public ProductPrice ProductPrice { get; set; }
    
}
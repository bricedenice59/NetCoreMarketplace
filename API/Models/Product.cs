using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }

    [ForeignKey("ProductStockId")]
    public Guid ProductStockId { get; set; }
    public ProductStock ProductStock { get; set; }
    
    [ForeignKey("ProductTypeId")]
    public Guid ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
}
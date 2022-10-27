using System.ComponentModel.DataAnnotations;

namespace API.Model;

public class ProductStock
{
    [Key]
    public Guid Id { get; set; }
    
    public int ProductsLeft { get; set; }
}
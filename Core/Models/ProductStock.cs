using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class ProductStock
{
    [Key]
    public Guid Id { get; set; }
    
    public int ProductsLeft { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class ProductStock : BaseModel
{
    [Required]
    public int ProductsLeft { get; set; }
}
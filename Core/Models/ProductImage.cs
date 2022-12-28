using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class ProductImage : BaseModel
{
    [Required]
    public Guid ProductId { get; set; }
    
    [Required]
    public string Url{ get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class ProductType : BaseModel
{
    [Required]
    public string Kind{ get; set; }
}
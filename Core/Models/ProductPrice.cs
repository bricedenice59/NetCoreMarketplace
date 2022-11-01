using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class ProductPrice : BaseModel
{
    [Required]
    public double PriceWithExcludedVAT { get; set; }
}
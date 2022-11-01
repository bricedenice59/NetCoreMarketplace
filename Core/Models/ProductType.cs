using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class ProductType
{
    [Key]
    public Guid Id { get; set; }
    
    public string Kind{ get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace API.Model;

public class ProductType
{
    [Key]
    public Guid Id { get; set; }
    
    public string Kind{ get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class BaseModel
{
    [Required]
    public Guid Id { get; set; }
}
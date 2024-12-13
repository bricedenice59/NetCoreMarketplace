using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace API.Dtos;

public class UserLoginDto
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
}
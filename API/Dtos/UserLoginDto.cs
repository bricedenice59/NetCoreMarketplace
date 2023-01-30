using Core.Models;

namespace API.Dtos;

public class UserLoginDto
{
    public string Email { get; set; }
    
    public string Password { get; set; }
    
}
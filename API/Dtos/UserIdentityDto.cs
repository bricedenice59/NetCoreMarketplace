using Core.Models;

namespace API.Dtos;

public class UserIdentityDto
{
    public string Token { get; set; }
    
    public string FirstName { get; set; }
    
    public string Email { get; set; }
    
}
using Microsoft.AspNetCore.Identity;

namespace Core.Models.Identity;

public class User : IdentityUser
{
    public string DisplayName { get; set; }
    public UserAddress Address { get; set; }
}
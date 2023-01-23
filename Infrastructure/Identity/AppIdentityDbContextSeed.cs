using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed 
{
    public static async Task SeedAsync(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                DisplayName = "Bob",
                Email = "bob@test.com",
                UserName = "bob@test.com",
                Address = new UserAddress
                {
                    FirstName = "Bob",
                    LastName = "Bobbity",
                    Street = "10 The street",
                    City = "New York",
                    State = "NY",
                    ZipCode = "90210"
                }
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}

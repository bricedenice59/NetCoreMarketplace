using Core.Models.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, 
        IConfiguration config)
    {
        var builder = services.AddIdentityCore<User>();

        builder = new IdentityBuilder(builder.UserType, builder.Services);
        builder.AddEntityFrameworkStores<AppIdentityDbContext>();
        builder.AddSignInManager<SignInManager<User>>();
        
        //Override the configuration 
        services.Configure<IdentityOptions>(opt =>
        {
            // Password settings 
            opt.Password.RequireDigit = false;
            opt.Password.RequiredLength = 8;
 
            // Lockout settings 
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
            opt.Lockout.MaxFailedAccessAttempts = 3;
 
            //Signin option
            opt.SignIn.RequireConfirmedEmail = false;
 
            // User settings 
            opt.User.RequireUniqueEmail = true;
 
            //Token Option
            //opt.Tokens.AuthenticatorTokenProvider = "Name of AuthenticatorTokenProvider";
 
        });

        services.AddAuthentication();
        return services;
    }
}
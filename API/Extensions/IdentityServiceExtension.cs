using System.Text;
using Core.Models.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
        });
        
        if (config["Auth0:Secret"] == null)
            throw new ArgumentNullException("Setting is missing: Auth0:Secret");
        if (config["Auth0:TokenExpirationDelay"] == null)
            throw new ArgumentNullException("Setting is missing: Auth0:TokenExpirationDelay");
        if (config["Auth0:Issuer"] == null)
            throw new ArgumentNullException("Setting is missing: Auth0:Issuer");
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Auth0:Secret"])),
                    ValidIssuer = config["Auth0:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

        return services;
    }
}
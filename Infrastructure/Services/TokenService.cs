using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Interfaces;
using Core.Models.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _symmetricSecurityKey;
    private readonly double _expirationDelay;
    private readonly string _issuer;
    
    public TokenService(IConfiguration config)
    {
        if (config["Auth0:Secret"] == null)
            throw new ArgumentNullException("Setting is missing: Auth0:Secret");
        if (config["Auth0:TokenExpirationDelay"] == null)
            throw new ArgumentNullException("Setting is missing: Auth0:TokenExpirationDelay");
        if (config["Auth0:Issuer"] == null)
            throw new ArgumentNullException("Setting is missing: Auth0:Issuer");

        _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Auth0:Secret"]));
        _expirationDelay = Convert.ToDouble(config["Auth0:TokenExpirationDelay"]);
        _issuer = config["Auth0:Issuer"];
    }
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.DisplayName)
        };
    
        var credentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
    
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = DateTime.Now.ToUniversalTime(),
            Expires = DateTime.Now.AddSeconds(_expirationDelay).ToUniversalTime(),
            SigningCredentials = credentials,
            Issuer = _issuer
        };
    
        var tokenHandler = new JwtSecurityTokenHandler();
    
        var token = tokenHandler.CreateToken(tokenDescriptor);
    
        return tokenHandler.WriteToken(token);
    }
    
    public User? ValidateToken(string token)
    {
        if (token == null) 
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _symmetricSecurityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            
            var userEmail = jwtToken.Claims.First(x => x.Type == "email").Value;
            var displayName = jwtToken.Claims.First(x => x.Type == "given_name").Value;

            // return user id from JWT token if validation successful
            return new User() {Email = userEmail, DisplayName = displayName};
        }
        catch(Exception ex)
        {
            // return null if validation fails
            return null;
        }
    }
}
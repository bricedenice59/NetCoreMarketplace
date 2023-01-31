using Core.Models.Identity;

namespace Core.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);

    User? ValidateToken(string token);
}
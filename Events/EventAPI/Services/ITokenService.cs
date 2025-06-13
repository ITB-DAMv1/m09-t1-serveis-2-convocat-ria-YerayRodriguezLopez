using EventAPI.Models;

namespace EventAPI.Services
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);

    }
}

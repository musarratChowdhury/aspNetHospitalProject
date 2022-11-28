
using System.Security.Claims;

namespace Infrastructure.Services
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
}
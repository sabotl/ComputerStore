using System.Security.Claims;

namespace ComputerStore.Domain.Services
{
    public interface IAccessTokenService
    {
        string CreateAccessToken(Guid userId);
        ClaimsPrincipal ValidateToken(string token);
    }
}

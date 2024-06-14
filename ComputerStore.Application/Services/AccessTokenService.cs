using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComputerStore.Application.Services
{
    public class AccessTokenService:TokenServiceBase, Domain.Services.IAccessTokenService
    {
        private readonly TimeSpan _accessTokenExpiration;

        public AccessTokenService(string secretKey, TimeSpan accessTokenExpiration)
            : base(secretKey)
        {
            _accessTokenExpiration = accessTokenExpiration;
        }

        public string CreateAccessToken(Guid userId)
        {
            return CreateAccessToken(userId, _accessTokenExpiration);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            return principal;
        }
    }
}

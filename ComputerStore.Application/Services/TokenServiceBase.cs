using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ComputerStore.Application.Services
{
    public  class TokenServiceBase
    {
        protected readonly string _secretKey;
        protected readonly string _audience;
        protected readonly string _issuer;
        protected TokenServiceBase(string secretKey, string audience, string issuer)
        {
            _issuer = issuer;
            _secretKey = secretKey;
            _audience = audience;
        }

        protected string CreateAccessToken(Guid userId, TimeSpan expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _issuer,
                Audience = _audience,
                Subject = new ClaimsIdentity(new[] { new Claim("user_id", userId.ToString()) }),
                Expires = DateTime.UtcNow.Add(expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ComputerStore.Application.Services
{
    public  class TokenServiceBase
    {
        protected readonly string _secretKey;

        protected TokenServiceBase(string secretKey)
        {
            _secretKey = secretKey;
        }

        protected string CreateAccessToken(Guid userId, TimeSpan expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("user_id", userId.ToString()) }),
                Expires = DateTime.UtcNow.Add(expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

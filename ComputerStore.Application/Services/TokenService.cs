using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ComputerStore.Application.Services
{
    public class TokenService
    {
        private readonly Domain.Repositories.IUserRepostory _userRepostory;
        private readonly string _secretKey;
        private readonly TimeSpan _accessTokenExpiration;
        private readonly TimeSpan _refreshTokenExpiration;
        public TokenService(Domain.Repositories.IUserRepostory userRepostory, string secretKey, TimeSpan accessTokenExpiration, TimeSpan refreshTokenExpiration)
        {
            _userRepostory = userRepostory;
            _secretKey = secretKey;
            _accessTokenExpiration = accessTokenExpiration;
            _refreshTokenExpiration = refreshTokenExpiration;
        }

        public async Task<(string accessToken, string refreshToken)> CreateTokens(Guid userId)
        {
            var accessToken = CreateAccessToken(userId);

            var refreshToken = await CreateRefreshToken(userId);

            return (accessToken, refreshToken);
        }
        public async Task<string> RefreshAccessToken(string refreshToken)
        {
            var principal = await _userRepostory.GetCurrencyRefreshToken(refreshToken);
            Console.WriteLine(principal);
            if (principal == null)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }
            var newAccessToken = CreateAccessToken(principal.UserId);

            return newAccessToken;
        }
        private string CreateAccessToken(Guid userId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("user_id", userId.ToString()) }),
                    Expires = DateTime.UtcNow.Add(_accessTokenExpiration),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create access token.", ex);
            }
        }

        private async Task<string> CreateRefreshToken(Guid userId)
        {
            try
            {
                await _userRepostory.DeleteRefreshToken(userId);

                var refreshToken = GenerateRefreshToken();

                await _userRepostory.SaveRefreshToken(userId, refreshToken, DateTime.UtcNow.Add(_refreshTokenExpiration));

                return refreshToken;
            }catch(Exception ex)
            {
                throw new Exception("Failed to create refresh token.", ex);
            }
        }
        private string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomNumber);
            }
            return BitConverter.ToString(randomNumber).Replace("-", "");
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

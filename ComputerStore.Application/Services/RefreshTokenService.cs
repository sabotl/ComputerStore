using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ComputerStore.Application.Services
{
    public class RefreshTokenService:TokenServiceBase, Domain.Services.IRefreshTokenService
    {
        private readonly Domain.Repositories.IUserRepostory _userRepostory;
        private readonly TimeSpan _refreshTokenExpiration;

        public RefreshTokenService(Domain.Repositories.IUserRepostory userRepostory, string secretKey,string audience,string issuer, TimeSpan refreshTokenExpiration)
            : base(secretKey, audience, issuer)
        {
            _userRepostory = userRepostory;
            _refreshTokenExpiration = refreshTokenExpiration;
        }

        public async Task<string> CreateRefreshToken(Guid userId)
        {
            await _userRepostory.DeleteRefreshToken(userId);

            var refreshToken = GenerateRefreshToken();

            await _userRepostory.SaveRefreshToken(userId, refreshToken, DateTime.UtcNow.Add(_refreshTokenExpiration));

            return refreshToken;
        }

        public async Task<string> RefreshAccessToken(string refreshToken)
        {
            var principal = await _userRepostory.GetCurrencyRefreshToken(refreshToken);
            if (principal == null)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }
            var newAccessToken = CreateAccessToken(principal.UserId, _refreshTokenExpiration);

            return newAccessToken;
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
    }
}

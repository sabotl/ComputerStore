using ComputerStore.Domain.Entities;

namespace ComputerStore.Application.Mappers
{
    public class TokenMapper
    {
        public Domain.Entities.RefreshToken MapToDto(RefreshToken refreshToken)
        {
            return new Domain.Entities.RefreshToken
            {
                Id = refreshToken.Id,
                Token = refreshToken.Token,
                UserId = refreshToken.UserId,
                ExpiryDate = refreshToken.ExpiryDate
            };
        }

        public RefreshToken MapToEntity(DTOs.RefreshTokenDTO refreshTokenDto)
        {
            return new RefreshToken
            {
                Id = refreshTokenDto.Id,
                Token = refreshTokenDto.Token,
                UserId = refreshTokenDto.UserId,
                ExpiryDate = refreshTokenDto.ExpiryDate
            };
        }
    }
}

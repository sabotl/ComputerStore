using ComputerStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Infrastructure.Repository
{
    public class UserRepostiry: BaseRepository<Domain.Entities.User>, Domain.Repositories.IUserRepostory
    {
        public UserRepostiry(Data.AppDbContext appDbContext): base(appDbContext) 
        {
        
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u=> u.login == login || u.Email == login);
        }

        public async Task SaveRefreshToken(Guid id, string refreshToken, DateTime dateTime)
        {
            var newRefreshToken = new Domain.Entities.RefreshToken
            {
                UserId = id,
                Token = refreshToken,
                ExpiryDate = dateTime,
            };

            _context.refreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRefreshToken(Guid id)
        {
            var refreshToken = await _context.refreshTokens.FirstOrDefaultAsync(rt => rt.UserId == id);

            if (refreshToken != null)
            {
                _context.refreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RefreshToken?> GetCurrencyRefreshToken(string refreshToken)
        {
            return await _context.refreshTokens.FirstOrDefaultAsync(i=> i.Token == refreshToken);
        }
    }
}

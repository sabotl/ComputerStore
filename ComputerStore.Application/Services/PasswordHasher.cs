using System.Security.Cryptography;
using System.Text;

namespace ComputerStore.Application.Services
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static string GenerateSalt(int size = 32)
        {
            var rng = new RNGCryptoServiceProvider();
            var saltBytes = new byte[size];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string salt)
        {
            var hashOfEnteredPassword = HashPassword(enteredPassword, salt);
            return hashOfEnteredPassword == storedHash;
        }
    }
}

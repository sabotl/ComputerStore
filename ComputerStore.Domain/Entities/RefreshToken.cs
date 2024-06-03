using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Domain.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiryDate { get; set; }

        public User User { get; set; }
    }
}

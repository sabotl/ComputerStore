using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Domain.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Guid user_id { get; set; }
        [Required]
        public DateTime Created_at { get; set; } = DateTime.Now;

        public ICollection<CartItems> CartItems { get; set; } = new List<CartItems>();
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ComputerStore.Domain.Entities
{
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, NotNull]
        public string Name { get; set; }

        [Required, NotNull]
        public string Description { get; set; }

        [Required, Timestamp, NotNull]
        public TimeSpan OpeningTime { get; set; }

        [Required, Timestamp, NotNull]
        public TimeSpan ClosingTime { get; set; }

        public ICollection<Goods> Products { get; set; } = new List<Goods>();
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ComputerStore.Domain.Entities
{
    public class Goods
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required, NotNull]
        public string Productname { get; set; }

        [Required, NotNull]
        public string Description { get; set; }

        [Required, NotNull]
        public decimal Price { get; set; }

        [Required, NotNull]
        public int Quantity { get; set; }

        [Required, ForeignKey("Shop"), NotNull]
        public int ShopId { get; set; }
        [Required, ForeignKey("GoodsCategory"), NotNull]
        public int CategoryID { get; set; }

        [JsonIgnore]
        public GoodsCategory GoodsCategory { get; set; }
        [JsonIgnore]
        public Shop ProductShop { get; set; }
    }
}

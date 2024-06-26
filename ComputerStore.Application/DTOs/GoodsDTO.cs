namespace ComputerStore.Application.DTOs
{
    public class GoodsDTO
    {
        public int Id { get; set; }
        public string Productname {  get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public int Quantity {  get; set; }
        public int ShopID {  get; set; }
    }
}

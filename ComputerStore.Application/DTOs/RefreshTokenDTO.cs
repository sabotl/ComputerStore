namespace ComputerStore.Application.DTOs
{
    public class RefreshTokenDTO
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Application.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        [MinLength(4)]
        public string? Login { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public bool IsAnonymous { get; set; }

    }
}

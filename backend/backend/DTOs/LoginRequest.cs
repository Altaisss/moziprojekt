using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Jelszo { get; set; } = null!;
    }
}

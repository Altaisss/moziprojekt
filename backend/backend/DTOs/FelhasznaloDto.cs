using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class FelhasznaloRequest
    {
        [Required]
        public string Nev { get; set; } = null!;

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(256)]
        public string Jelszo { get; set; } = null!;
    }

    public class FelhasznaloResponse
    {
        public int Id { get; set; }
        public string Nev { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Jelszo { get; set; } = null!;
    }
}

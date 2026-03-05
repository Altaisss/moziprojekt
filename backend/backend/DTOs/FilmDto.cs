using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class FilmRequest
    {
        [Required]
        [StringLength(200)]
        public string Cim { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Rendezo { get; set; } = null!;

        public int Hossz { get; set; }

        public string? Leiras { get; set; }
    }

    public class FilmResponse
    {
        public int Id { get; set; }
        public string Cim { get; set; } = null!;
        public string Rendezo { get; set; } = null!;
        public int Hossz { get; set; }
        public string? Leiras { get; set; }
    }
}

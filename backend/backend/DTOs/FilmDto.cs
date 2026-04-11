using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class FilmRequest
    {
        [Required(ErrorMessage = "A cím megadása kötelező")]
        [StringLength(200, MinimumLength = 1)]
        public string Cim { get; set; } = null!;

        [Required(ErrorMessage = "A rendező megadása kötelező")]
        [StringLength(200, MinimumLength = 2)]
        public string Rendezo { get; set; } = null!;

        [Range(1, 500, ErrorMessage = "A hossz 1 és 500 perc között kell legyen")]
        public int Hossz { get; set; }

        [StringLength(2000, ErrorMessage = "A leírás túl hosszú")]
        public string? Leiras { get; set; }
        public IFormFile? Kep { get; set; }
    }

    public class FilmResponse
    {
        public int Id { get; set; }
        public string Cim { get; set; } = null!;
        public string Rendezo { get; set; } = null!;
        public int Hossz { get; set; }
        public string? Leiras { get; set; }
        public string? KepUrl { get; set; }
    }
}

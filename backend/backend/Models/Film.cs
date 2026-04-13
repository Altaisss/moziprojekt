using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public int Hossz { get; set; }
        public string? Leiras { get; set; }
        [Required]
        [StringLength(200)]
        public string Cim { get; set; } = null!;
        [Required]
        [StringLength(200)]
        public string Rendezo { get; set; } = null!;
        public string? KepUrl { get; set; }
        public ICollection<Vetites> Vetitesek { get; set; } = new List<Vetites>(); 
    }
}

using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Felhasznalo
    {
        [Required]
        public int Id { get; set; } 
        public string Nev { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Jelszo { get; set; } = null!;
        public ICollection<Foglalas> Foglalt = new List<Foglalas>();
    }
}

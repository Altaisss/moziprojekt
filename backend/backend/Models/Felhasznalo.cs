using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Felhasznalo
    {
        [Key]
        public int Id { get; set; } 
        public string Nev { get; set; } = null!;
        [StringLength(200)]
        public string Email { get; set; } = null!;
        [StringLength(256)]
        public string Jelszo { get; set; } = null!;
        public ICollection<Foglalas> Foglalasok = new List<Foglalas>();
    }
}

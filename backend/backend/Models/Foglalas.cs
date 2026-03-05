using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Foglalas
    {
        [Key]
        public int Id { get; set; }
        public int FelhasznaloId { get; set; }
        public Felhasznalo Felhasznalo { get; set; } = null!;
        public ICollection<Foglalthely> Foglalthely { get; set; } = new List<Foglalthely>(); // Fix #6: field → property
    }
}

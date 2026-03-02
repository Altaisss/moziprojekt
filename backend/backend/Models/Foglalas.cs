using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Foglalas
    {
        public int Id { get; set; }
        public int FelhasznaloId { get; set; }
        public Felhasznalo Felhasznalo { get; set; } = null!;
        public ICollection<Foglalthely> Foglalt = new List<Foglalthely>();

    }
}

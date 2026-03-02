using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Foglalthely
    {
        [Key]
        public int Id { get; set; }
        public int SzekId { get; set; }
        public Szek Szek { get; set; } = null!;
        public int FoglalasId { get; set; }
        public Foglalas Foglalas { get; set; } = null!;
        public int VetitesId { get; set; }
        public Vetites Vetites { get; set; } = null!;
    }
}

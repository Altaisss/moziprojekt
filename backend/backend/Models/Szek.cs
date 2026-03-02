using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Szek
    {
        [Key]
        public int Id { get; set; }
        public int Sor {  get; set; }
        public int Szam { get; set; }
        public char Oldal { get; set; }
        public int TeremId { get; set; }
        public Terem Terem { get; set; } = null!;
        public ICollection<Foglalthely> Foglalthely = new List<Foglalthely>();
    }
}

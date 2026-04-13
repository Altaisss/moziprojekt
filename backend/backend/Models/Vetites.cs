using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Vetites
    {
        [Key]
        public int Id { get; set; }
        public DateTime Idopont { get; set; }
        public int TeremId { get; set; }
        public Terem Terem { get; set; } = null!;
        public int FilmId { get; set; }
        public Film Film { get; set; } = null!;

        public int JegyAr { get; set; }
        public string? Nyelv { get; set; }

        public ICollection<Foglalthely> Foglalthely { get; set; } = new List<Foglalthely>(); 
    }
}

using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Terem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TeremNev { get; set; } = null!;

        public ICollection<Vetites> Vetitesek { get; set; } = new List<Vetites>(); 
        public ICollection<Szek> Szekek { get; set; } = new List<Szek>();         
    }
}

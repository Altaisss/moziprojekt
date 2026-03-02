using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Terem
    {
        [Required]
        public string Id { get; set; }
        public string TeremNev { get; set; }

        public ICollection<Vetites> Vetitesek = new List<Vetites>();
        public ICollection<Szek> Szekek = new List<Szek>(); 
    }
}

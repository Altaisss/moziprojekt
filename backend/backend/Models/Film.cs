using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Film
    {
        public int Id { get; set; }
        public int Hossz { get; set; }
        public string Leiras { get; set; }
        public string Cim {  get; set; }
        public string Rendezo { get; set; }
        public ICollection<Vetites> Vetitesek { get; set; } = new List<Vetites>();
        

    }
}

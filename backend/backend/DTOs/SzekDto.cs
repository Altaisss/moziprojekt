using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class SzekRequest
    {
        [Required]
        public int Sor { get; set; }

        [Required]
        public int Szam { get; set; }

        [Required]
        public char Oldal { get; set; }

        [Required]
        public int TeremId { get; set; }
    }

    public class SzekResponse
    {
        public int Id { get; set; }
        public int Sor { get; set; }
        public int Szam { get; set; }
        public char Oldal { get; set; }
        public int TeremId { get; set; }
    }
}

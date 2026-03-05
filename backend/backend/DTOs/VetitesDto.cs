using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class VetitesRequest
    {
        [Required]
        public DateTime Idopont { get; set; }

        [Required]
        public int TeremId { get; set; }

        [Required]
        public int FilmId { get; set; }

        [Required]
        public int JegyAr { get; set; }

        public string? Nyelv { get; set; }

        public string? VetitesTipus { get; set; }
    }

    public class VetitesResponse
    {
        public int Id { get; set; }
        public DateTime Idopont { get; set; }
        public int TeremId { get; set; }
        public int FilmId { get; set; }
        public int JegyAr { get; set; }
        public string? Nyelv { get; set; }
        public string? VetitesTipus { get; set; }
    }
}

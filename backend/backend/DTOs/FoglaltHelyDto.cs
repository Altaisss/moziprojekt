using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class FoglaltHelyRequest
    {
        [Required]
        public int SzekId { get; set; }

        [Required]
        public int FoglalasId { get; set; }

        [Required]
        public int VetitesId { get; set; }
    }

    public class FoglaltHelyResponse
    {
        public int Id { get; set; }
        public int SzekId { get; set; }
        public int FoglalasId { get; set; }
        public int VetitesId { get; set; }
    }
}

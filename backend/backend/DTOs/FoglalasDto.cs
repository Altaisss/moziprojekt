using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class FoglalasRequest
    {
        [Required]
        public int FelhasznaloId { get; set; }
    }

    public class FoglalasResponse
    {
        public int Id { get; set; }
        public int FelhasznaloId { get; set; }
    }
}

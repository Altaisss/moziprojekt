using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class TeremRequest
    {
        [Required]
        public string TeremNev { get; set; } = null!;
    }

    public class TeremResponse
    {
        public int Id { get; set; }
        public string TeremNev { get; set; } = null!;
    }
}

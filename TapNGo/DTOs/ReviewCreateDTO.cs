using System.ComponentModel.DataAnnotations;

namespace TapNGo.DTOs
{
    public class ReviewCreateDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        [Range(1,5,ErrorMessage = "Review must be between 1 - 5.")]
        public int? Rating { get; set; }

        public string? Comment { get; set; }
    }
}

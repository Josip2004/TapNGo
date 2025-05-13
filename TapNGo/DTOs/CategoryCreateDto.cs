using System.ComponentModel.DataAnnotations;

namespace TapNGo.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}

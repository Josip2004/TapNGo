using System.ComponentModel.DataAnnotations;

namespace TapNGo.DTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}

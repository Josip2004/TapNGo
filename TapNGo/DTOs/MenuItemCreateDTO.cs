using System.ComponentModel.DataAnnotations;

namespace TapNGo.DTOs
{
    public class MenuItemCreateDTO
    {
        public int UserId { get; set; }

        public int MenuCategoryId { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}

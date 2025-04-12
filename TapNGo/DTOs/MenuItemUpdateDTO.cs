using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TapNGo.DTOs
{
    public class MenuItemUpdateDTO
    {
        public int MenuCategoryId { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        [Column("Image_url")]
        [StringLength(255)]
        public string? ImageUrl { get; set; }
    }
}

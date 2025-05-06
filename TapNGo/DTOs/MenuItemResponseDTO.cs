using System.ComponentModel.DataAnnotations.Schema;

namespace TapNGo.DTOs
{
    public class MenuItemResponseDTO
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}

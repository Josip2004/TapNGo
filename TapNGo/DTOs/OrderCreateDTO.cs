// DTOs/OrderCreateDTO.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace TapNGo.DTOs
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }

        public string? Note { get; set; }

        [Column(TypeName = "decimal(18, 0)")]

        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}

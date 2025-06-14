using System.ComponentModel.DataAnnotations.Schema;

namespace TapNGo.DTOs
{
    public class OrderCreateDTO
    {
        public int? UserId { get; set; }

        public string? Note { get; set; }
        public int TableNumber { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}

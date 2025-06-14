using System.ComponentModel.DataAnnotations.Schema;

namespace TapNGo.DTOs
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Status { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }
        public int TableNumber { get; set; }
        public string? Note { get; set; }

        public List<OrderItemDetailDTO> OrderItems { get; set; } = new();
    }

    public class OrderItemDetailDTO
    {
        public int MenuItemId { get; set; }

        public string MenuItemName { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal PricePerItem { get; set; }
    }
}

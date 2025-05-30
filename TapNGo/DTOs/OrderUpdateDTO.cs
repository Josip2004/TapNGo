// DTOs/OrderUpdateDTO.cs
using System.ComponentModel.DataAnnotations;

namespace TapNGo.DTOs
{
    public class OrderUpdateDTO
    {
        public int Status { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        // Optional: Only include if updating items is supported in PUT
        public List<OrderItemDTO>? OrderItems { get; set; }
    }
}

// DTOs/OrderItemDTO.cs
using System.ComponentModel.DataAnnotations;

namespace TapNGo.DTOs
{
    public class OrderItemDTO
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}

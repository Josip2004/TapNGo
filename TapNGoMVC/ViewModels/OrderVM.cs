using System.ComponentModel.DataAnnotations.Schema;
using TapNGo.DAL.Models;

namespace TapNGoMVC.ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int Status { get; set; }

        public decimal TotalPrice { get; set; }

        public string? Note { get; set; }

        public int TableNumber { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}

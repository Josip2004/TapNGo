using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapNGo.DAL.Models;

[Table("OrderItem")]
public partial class OrderItem
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int MenuItemId { get; set; }

    public int? Quantity { get; set; }

    [ForeignKey("MenuItemId")]
    [InverseProperty("OrderItems")]
    public virtual MenuItem MenuItem { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;
}

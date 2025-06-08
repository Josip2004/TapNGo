using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapNGo.DAL.Models;

[Table("MenuItem")]
public partial class MenuItem
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int MenuCategoryId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column("Image_url")]
    [StringLength(256)]
    public string? ImageUrl { get; set; }

    [ForeignKey("MenuCategoryId")]
    [InverseProperty("MenuItems")]
    public virtual MenuCategory MenuCategory { get; set; } = null!;

    [InverseProperty("MenuItem")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("UserId")]
    [InverseProperty("MenuItems")]
    public virtual User User { get; set; } = null!;
}

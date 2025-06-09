using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapNGo.DAL.Models;

[Table("Review")]
public partial class Review
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Reviews")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Reviews")]
    public virtual User User { get; set; } = null!;
}

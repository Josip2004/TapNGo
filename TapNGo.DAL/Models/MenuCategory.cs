using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapNGo.DAL.Models;

[Table("MenuCategory")]
public partial class MenuCategory
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("MenuCategory")]
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}

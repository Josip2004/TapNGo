using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TapNGo.Models;

[Table("User")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string PwdHash { get; set; } = null!;

    [StringLength(255)]
    public string PwdSalt { get; set; } = null!;

    [StringLength(50)]
    public string Username { get; set; } = null!;

    public int RoleId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}

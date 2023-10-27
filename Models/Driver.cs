using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1Project.Models;

[Table("driver_standings")]
public partial class Driver
{
    [Key]
    [Column("id")]
    [StringLength(32)]
    public string Id { get; set; } = null!;
    
    [Column("name")]
    [StringLength(128)]
    public string Name { get; set; } = null!;

    [Column("position")]
    public short Position { get; set; }

    [Column("flag")]
    [StringLength(32)]
    public string? Flag { get; set; }

    [Column("points")]
    public short Points { get; set; }
}

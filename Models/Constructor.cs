using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1Project.Models;

[Table("constructor_standings")]
public partial class Constructor
{
    [Key]
    [Column("name")]
    [StringLength(128)]
    public string Name { get; set; } = null!;

    [Column("position")]
    public short Position { get; set; }

    [Column("logo")]
    public string? Logo { get; set; }

    [Column("points")]
    public short Points { get; set; }
}

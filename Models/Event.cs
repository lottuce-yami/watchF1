using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1Project.Models;

[Table("schedule")]
public partial class Event
{
    [Key]
    [Column("time")]
    public DateTime Time { get; set; }

    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

    [Column("featured")]
    public bool? Featured { get; set; }
}

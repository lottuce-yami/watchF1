using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1Project.Models;

[Table("schedule")]
public partial class Schedule
{
    [Key]
    [Column("id")]
    [StringLength(32)]
    public string Id { get; set; } = null!;

    [Column("title")]
    [StringLength(32)]
    public string Title { get; set; } = null!;

    [Column("time")]
    public DateTime Time { get; set; }

    [Column("featured")]
    public bool Featured { get; set; }
}

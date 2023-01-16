using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1Project.Models;

[Table("users")]
public partial class User
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("subscription")]
    public short Subscription { get; set; }

    [Column("first_name")]
    [StringLength(64)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(64)]
    public string? LastName { get; set; }

    [Column("username")]
    [StringLength(32)]
    public string? Username { get; set; }

    [Column("photo")]
    public string? Photo { get; set; }
}

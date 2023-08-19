using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Models;

[Table("constructor_standings")]
public partial class ConstructorStanding
{
    [Column("position")]
    public short Position { get; set; }

    [Key]
    [Column("name")]
    [StringLength(128)]
    public string Name { get; set; } = null!;

    [Column("logo")]
    public string? Logo { get; set; }

    [Column("points")]
    public short Points { get; set; }
}

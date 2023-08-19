using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Models;

[Table("videos")]
public partial class Video
{
    [Key]
    [Column("id")]
    [StringLength(8)]
    public string Id { get; set; } = null!;

    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

    [Column("championship")]
    [StringLength(64)]
    public string? Championship { get; set; }

    [Column("season")]
    public short? Season { get; set; }

    [Column("grand_prix")]
    [StringLength(64)]
    public string? GrandPrix { get; set; }

    [Column("type")]
    [StringLength(32)]
    public string? Type { get; set; }
}

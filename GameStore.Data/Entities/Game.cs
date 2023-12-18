using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Entities;

public class Game
{
    public int Id { get; set; }

    [NotNull]
    [StringLength(100)]
    [MinLength(3)]
    public required string Name { get; set; }

    [NotNull]
    [StringLength(100)]
    [MinLength(3)]
    public required string Genre { get; set; }

    [Range(1, 100)]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    public DateTime ReleaseDate { get; set; }

    [Url]
    [StringLength(100)]
    public required string ImageUri { get; set; }
}

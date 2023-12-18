

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameStore.Api.Dtos;

public record UpdateGameDto(
    [NotNull]
    [StringLength(100)]
    [MinLength(3)]
    string Name,

    [NotNull]
    [StringLength(100)]
    [MinLength(3)]
    string Genre,

    [Range(1, 100)]
    decimal Price,
    DateTime ReleaseDate,

    [Url]
    [StringLength(100)]
    string ImageUri
);

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Dtos;
using GameStore.Data.Entities;

namespace GameStore.Api.Extensions;

public static class EntityExtension
{
    public static GameDto AsDto(this Game game)
    {
        return new GameDto(
            game.Id,
            game.Name,
            game.Genre,
            game.Price,
            game.ReleaseDate,
            game.ImageUri
        );
    }
}

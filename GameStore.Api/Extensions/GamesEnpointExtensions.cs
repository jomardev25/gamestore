using GameStore.Api.Dtos;
using GameStore.Api.Exceptions;
using GameStore.Api.Repositories;
using GameStore.Data.Entities;

namespace GameStore.Api.Extensions;

public static class GamesEnpointExtensions
{
    public static RouteGroupBuilder MapGamesEnpoints(this IEndpointRouteBuilder routes)
    {
        const string GetEndpointName = "MapGetEndpointName";
        var routeGroup = routes.MapGroup("/games")
                    .WithParameterValidation();

        routeGroup.MapGet("/", async (IGameRepository gameRepository) =>
        {
            return (await gameRepository.GetAllAsync()).Select(game => game.AsDto()).ToList();
        });

        routeGroup.MapGet("/{id}", async (int id, IGameRepository gameRepository) =>
                    {
                        var game = await gameRepository.GetAsync(id);
                        if (game is null) return Results.NotFound(new ResourceNotFoundException("Game", "id", id));
                        return Results.Ok(game.AsDto());

                    }).WithName(GetEndpointName);

        routeGroup.MapPost("/", async (CreateGameDto gameDto, IGameRepository gameRepository) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            await gameRepository.CreateAsync(game);
            return Results.CreatedAtRoute(GetEndpointName, new { id = game.Id }, game);
        });

        routeGroup.MapPut("/{id}", async (int id, UpdateGameDto gameDto, IGameRepository gameRepository) =>
        {

            var existingGame = await gameRepository.GetAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = gameDto.Name;
            existingGame.Genre = gameDto.Genre;
            existingGame.Price = gameDto.Price;
            existingGame.ReleaseDate = gameDto.ReleaseDate;
            existingGame.ImageUri = gameDto.ImageUri;
            await gameRepository.UpdateAsync(existingGame);

            return Results.Ok(existingGame);

        });

        routeGroup.MapDelete("/{id}", async (int id, IGameRepository gameRepository) =>
        {
            var existingGame = await gameRepository.GetAsync(id);

            if (existingGame is not null)
            {
                await gameRepository.DeleteAsync(id);
            }

            return Results.NoContent();
        });

        return routeGroup;
    }
}

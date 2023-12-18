using GameStore.Api.Exceptions;
using GameStore.Api.Extensions;
using GameStore.Data;
using GameStore.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

const string GetEndpointName = "GetEndpointName";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

/* app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Test}/{action=Index}"
); */
app.Services.InitializeDbAsync();
app.MapGamesEnpoints();
app.MapControllers();

var routeGroup = app.MapGroup("/api/games")
                    .WithParameterValidation();

routeGroup.MapGet("/{id}", async (int id, GameStoreDbContext dbContext) =>
{
    var game = await dbContext.Games.FindAsync(id);
    if (game is null) return Results.NotFound(new ResourceNotFoundException("Game", "id", id));
    return Results.Ok(game);

}).WithName(GetEndpointName);

routeGroup.MapPost("/", async (Game game, GameStoreDbContext dbContext) =>
{
    dbContext.Games.Add(game);
    await dbContext.SaveChangesAsync();
    return Results.CreatedAtRoute(GetEndpointName, new { id = game.Id }, game);
});

app.Run();

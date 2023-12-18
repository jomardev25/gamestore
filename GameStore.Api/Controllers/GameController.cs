using GameStore.Data;
using GameStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Controllers;
[ApiController]
[Route("api/[controller]s")]
public class GameController : ControllerBase
{
    private readonly GameStoreDbContext _dbContext;
    public GameController(GameStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Game>> GetAllGamesAsync()
    {
        return await _dbContext.Games.AsNoTracking().ToListAsync();
    }

    /* [HttpGet("{id}")]
    public async Task<Game?> GetGameByIdAsync(int id)
    {
        var game = _games.Find(g => g.Id == id);
        if (game is null) Results.NoContent();
        return await Task.FromResult(game);
    } */

    /* [HttpPost]
    public async Task<Game> CreateGameAsync(Game game)
    {
        game.Id = _games.Max(g => g.Id) + 1;
        _games.Add(game);

        return await Task.FromResult(game);
    } */

    [HttpPatch("{id}")]
    public async Task<ActionResult<Game>> UpdateGameAsync(int id, Game game)
    {
        var existingGame = await _dbContext.Games.FindAsync(id);

        if (existingGame is null)
        {
            return NotFound();
        }

        existingGame.Name = game.Name;
        existingGame.Genre = game.Genre;
        existingGame.Price = game.Price;
        existingGame.ReleaseDate = game.ReleaseDate;
        existingGame.ImageUri = game.ImageUri;
        await _dbContext.SaveChangesAsync();

        return Ok(existingGame);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGameAsync(int id)
    {
        var existingGame = await _dbContext.Games.FindAsync(id);
        if (existingGame is null)
        {
            return NotFound();
        }

        await _dbContext.Games.Where(g => g.Id == id).ExecuteDeleteAsync();

        return NoContent();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data;
using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace GameStore.Api.Repositories;

public class EFCoreGameRepository : IGameRepository
{
    private readonly GameStoreDbContext _dbContext;

    public EFCoreGameRepository(GameStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Game>> GetAllAsync()
    {
        return await _dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await _dbContext.Games.FindAsync(id);
    }

    public async Task CreateAsync(Game game)
    {
        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game game)
    {
        _dbContext.Games.Update(game);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _dbContext.Games.Where(game => game.Id == id)
                            .ExecuteDeleteAsync();
    }
}

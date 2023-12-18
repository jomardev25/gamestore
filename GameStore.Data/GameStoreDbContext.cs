using GameStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;
public partial class GameStoreDbContext : DbContext
{
    public GameStoreDbContext()
    {

    }
    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Game> Games { get; set; }
}

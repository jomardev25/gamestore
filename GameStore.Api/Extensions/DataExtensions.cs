using GameStore.Api.Repositories;
using GameStore.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Extensions;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("GameStoreContext");
        services.AddMySql<GameStoreDbContext>(connString, ServerVersion.AutoDetect(connString))
                .AddScoped<IGameRepository, EFCoreGameRepository>();
        return services;
    }
}

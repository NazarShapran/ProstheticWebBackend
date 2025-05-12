
using Infrastructure.Persistence;

namespace Api.Modules;

public static class DbModule
{
    public static async Task InitialiseDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initializer.InitialiseAsync();
        var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        
        if (bool.Parse(config["AllowSeeders"]!))
        {
            await seeder.SeedAsync();
        }
    }
}
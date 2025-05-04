using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitialiser(ApplicationDbContext context)
{
    public async Task InitialiseAsync()
    {
        await context.Database.MigrateAsync();
    }
}
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.AmputationLevels;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class AmputationLevelRepository(ApplicationDbContext context): IAmputationLevelRepository, IAmputationLevelQueries
{
    public async Task<IReadOnlyList<AmputationLevel>> GetAll(CancellationToken cancellationToken)
    {
        return await context.AmputationLevels
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Option<AmputationLevel>> SearchByTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await context.AmputationLevels
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);

        return entity == null ? Option.None<AmputationLevel>() : Option.Some(entity);
    }
    
    public async Task<Option<AmputationLevel>> GetById(AmputationLevelId id, CancellationToken cancellationToken)
    {
        var entity = await context.AmputationLevels
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<AmputationLevel>() : Option.Some(entity);
    }
    
    public async Task<AmputationLevel> Add(AmputationLevel amputationLevel, CancellationToken cancellationToken)
    {
        await context.AmputationLevels.AddAsync(amputationLevel, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return amputationLevel;
    }

    public async Task<AmputationLevel> Update(AmputationLevel amputationLevel, CancellationToken cancellationToken)
    {
        context.AmputationLevels.Update(amputationLevel);

        await context.SaveChangesAsync(cancellationToken);

        return amputationLevel;
    }
    public async Task<AmputationLevel> Delete(AmputationLevel amputationLevel, CancellationToken cancellationToken)
    {
        context.AmputationLevels.Remove(amputationLevel);
        await context.SaveChangesAsync(cancellationToken);
        return amputationLevel;
    }
}
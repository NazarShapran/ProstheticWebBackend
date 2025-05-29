using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.ProstheticStatuses;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ProstheticStatusRepository(ApplicationDbContext context) : IProstheticStatusRepository, IProstheticStatusQueries
{
    public async Task<Option<ProstheticStatus>> GetById(ProstheticStatusId id, CancellationToken cancellationToken)
    {
        var entity = await context.ProstheticStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<ProstheticStatus>() : Option.Some(entity);
    }

    public async Task<IReadOnlyList<ProstheticStatus>> GetAll(CancellationToken cancellationToken)
    {
        return await context.ProstheticStatuses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<ProstheticStatus>> SearchByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.ProstheticStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Title == name, cancellationToken);

        return entity == null ? Option.None<ProstheticStatus>() : Option.Some(entity);
    }

    public async Task<ProstheticStatus> Create(ProstheticStatus status, CancellationToken cancellationToken)
    {
        await context.ProstheticStatuses.AddAsync(status, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return status;
    }

    public async Task<ProstheticStatus> Update(ProstheticStatus status, CancellationToken cancellationToken)
    {
        context.ProstheticStatuses.Update(status);
        await context.SaveChangesAsync(cancellationToken);
        return status;
    }

    public async Task<ProstheticStatus> Delete(ProstheticStatus status, CancellationToken cancellationToken)
    {
        context.ProstheticStatuses.Remove(status);
        await context.SaveChangesAsync(cancellationToken);
        return status;
    }
} 
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Prosthetics;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ProstheticRepository(ApplicationDbContext context) : IProstheticRepository, IProstheticQueries
{
    public async Task<IReadOnlyList<Prosthetic>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Prosthetics
            .Include(t => t.Type)
            .Include(m => m.Material)
            .Include(f => f.Functionality)
            .Include(a => a.AmputationLevel)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Prosthetic>> SearchByTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await context.Prosthetics
            .Include(t => t.Type)
            .Include(m => m.Material)
            .Include(f => f.Functionality)
            .Include(a => a.AmputationLevel)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);

        return entity == null ? Option.None<Prosthetic>() : Option.Some(entity);
    }

    public async Task<Option<Prosthetic>> GetById(ProstheticId id, CancellationToken cancellationToken)
    {
        var entity = await context.Prosthetics
            .Include(t => t.Type)
            .Include(m => m.Material)
            .Include(f => f.Functionality)
            .Include(a => a.AmputationLevel)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Prosthetic>() : Option.Some(entity);
    }

    public async Task<Prosthetic> Add(Prosthetic prosthetic, CancellationToken cancellationToken)
    {
        await context.Prosthetics.AddAsync(prosthetic, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return prosthetic;
    }

    public async Task<Prosthetic> Update(Prosthetic prosthetic, CancellationToken cancellationToken)
    {
        context.Prosthetics.Update(prosthetic);

        await context.SaveChangesAsync(cancellationToken);

        return prosthetic;
    }

    public async Task<Prosthetic> Delete(Prosthetic prosthetic, CancellationToken cancellationToken)
    {
        context.Prosthetics.Remove(prosthetic);
        await context.SaveChangesAsync(cancellationToken);
        return prosthetic;
    }
}
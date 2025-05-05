using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Functionalities;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class FunctionalityRepository(ApplicationDbContext context): IFunctionalityRepository, IFunctionalityQueries
{
    
    public async Task<IReadOnlyList<Functionality>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Functionalities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Option<Functionality>> SearchByTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await context.Functionalities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);

        return entity == null ? Option.None<Functionality>() : Option.Some(entity);
    }
    
    public async Task<Option<Functionality>> GetById(FunctionalityId id, CancellationToken cancellationToken)
    {
        var entity = await context.Functionalities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Functionality>() : Option.Some(entity);
    }
    
    public async Task<Functionality> Add(Functionality functionality, CancellationToken cancellationToken)
    {
        await context.Functionalities.AddAsync(functionality, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return functionality;
    }

    public async Task<Functionality> Update(Functionality functionality, CancellationToken cancellationToken)
    {
        context.Functionalities.Update(functionality);

        await context.SaveChangesAsync(cancellationToken);

        return functionality;
    }
    public async Task<Functionality> Delete(Functionality functionality, CancellationToken cancellationToken)
    {
        context.Functionalities.Remove(functionality);
        await context.SaveChangesAsync(cancellationToken);
        return functionality;
    }
}
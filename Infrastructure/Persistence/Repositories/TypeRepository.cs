using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.ProstheticTypes;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class TypeRepository(ApplicationDbContext context): ITypeRepository, ITypeQueries
{
    public async Task<IReadOnlyList<ProstheticType>> GetAll(CancellationToken cancellationToken)
    {
        return await context.ProstheticTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Option<ProstheticType>> SearchByTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await context.ProstheticTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);

        return entity == null ? Option.None<ProstheticType>() : Option.Some(entity);
    }
    
    public async Task<Option<ProstheticType>> GetById(TypeId id, CancellationToken cancellationToken)
    {
        var entity = await context.ProstheticTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<ProstheticType>() : Option.Some(entity);
    }
    
    public async Task<ProstheticType> Add(ProstheticType type, CancellationToken cancellationToken)
    {
        await context.ProstheticTypes.AddAsync(type, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return type;
    }

    public async Task<ProstheticType> Update(ProstheticType type, CancellationToken cancellationToken)
    {
        context.ProstheticTypes.Update(type);

        await context.SaveChangesAsync(cancellationToken);

        return type;
    }
    public async Task<ProstheticType> Delete(ProstheticType type, CancellationToken cancellationToken)
    {
        context.ProstheticTypes.Remove(type);
        await context.SaveChangesAsync(cancellationToken);
        return type;
    }
}
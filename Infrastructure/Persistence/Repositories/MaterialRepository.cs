using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Materials;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class MaterialRepository(ApplicationDbContext context): IMaterialRepository, IMaterialQueries
{
    public async Task<IReadOnlyList<Material>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Materials
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Option<Material>> SearchByTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await context.Materials
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);

        return entity == null ? Option.None<Material>() : Option.Some(entity);
    }
    
    public async Task<Option<Material>> GetById(MaterialId id, CancellationToken cancellationToken)
    {
        var entity = await context.Materials
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Material>() : Option.Some(entity);
    }
    
    public async Task<Material> Add(Material material, CancellationToken cancellationToken)
    {
        await context.Materials.AddAsync(material, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return material;
    }

    public async Task<Material> Update(Material material, CancellationToken cancellationToken)
    {
        context.Materials.Update(material);

        await context.SaveChangesAsync(cancellationToken);

        return material;
    }
    public async Task<Material> Delete(Material material, CancellationToken cancellationToken)
    {
        context.Materials.Remove(material);
        await context.SaveChangesAsync(cancellationToken);
        return material;
    }
}
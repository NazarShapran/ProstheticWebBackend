
using Domain.Materials;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IMaterialRepository
{
    Task<Option<Material>> GetById(MaterialId id, CancellationToken cancellationToken);
    Task<Material> Add(Material material, CancellationToken cancellationToken);
    Task<Option<Material>> SearchByTitle(string title, CancellationToken cancellationToken);
    Task<Material> Update(Material material, CancellationToken cancellationToken);
    Task<Material> Delete(Material material, CancellationToken cancellationToken);
}
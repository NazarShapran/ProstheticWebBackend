using Domain.Materials;

namespace Application.Common.Interfaces.Queries;

public interface IMaterialQueries
{
    Task<IReadOnlyList<Material>> GetAll(CancellationToken cancellationToken);
}
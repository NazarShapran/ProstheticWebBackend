namespace Application.Common.Interfaces.Queries;
using Domain.Materials;

public interface IMaterialQueries
{
    Task<IReadOnlyList<Material>> GetAll(CancellationToken cancellationToken);
}
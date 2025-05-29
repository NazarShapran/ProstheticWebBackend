using Domain.Prosthetics;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IProstheticQueries
{
    Task<(IReadOnlyList<Prosthetic> Items, int TotalCount)> GetAllPaged(int page, int pageSize, CancellationToken cancellationToken);
    Task<Option<Prosthetic>> GetById(ProstheticId id, CancellationToken cancellationToken);
}
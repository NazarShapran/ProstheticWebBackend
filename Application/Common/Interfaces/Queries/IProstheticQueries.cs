using Domain.Prosthetics;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IProstheticQueries
{
    Task<IReadOnlyList<Prosthetic>> GetAll(CancellationToken cancellationToken);
    Task<Option<Prosthetic>> GetById(ProstheticId id, CancellationToken cancellationToken);
}
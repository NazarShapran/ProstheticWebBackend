using Domain.Types;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface ITypeQueries
{
    Task<IReadOnlyList<ProstheticType>> GetAll(CancellationToken cancellationToken);
    Task<Option<ProstheticType>> GetById(TypeId id, CancellationToken cancellationToken);
}
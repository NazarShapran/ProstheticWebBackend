using Optional;
namespace Application.Common.Interfaces.Queries;
using Domain.ProstheticTypes;

public interface ITypeQueries
{
    Task<IReadOnlyList<ProstheticType>> GetAll(CancellationToken cancellationToken);
    Task<Option<ProstheticType>> GetById(TypeId id, CancellationToken cancellationToken);
}
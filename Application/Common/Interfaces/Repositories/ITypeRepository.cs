using Domain.Types;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface ITypeRepository
{
    Task<Option<ProstheticType>> GetById(TypeId id, CancellationToken cancellationToken);
    Task<ProstheticType> Add(ProstheticType prostheticType, CancellationToken cancellationToken);
    Task<Option<ProstheticType>> SearchByTitle(string title, CancellationToken cancellationToken);
    Task<ProstheticType> Update(ProstheticType prostheticType, CancellationToken cancellationToken);
    Task<ProstheticType> Delete(ProstheticType prostheticType, CancellationToken cancellationToken);
}
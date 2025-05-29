using Domain.ProstheticStatuses;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IProstheticStatusRepository
{
    Task<Option<ProstheticStatus>> GetById(ProstheticStatusId id, CancellationToken cancellationToken);
    Task<ProstheticStatus> Create(ProstheticStatus status, CancellationToken cancellationToken);
    Task<ProstheticStatus> Update(ProstheticStatus status, CancellationToken cancellationToken);
    Task<ProstheticStatus> Delete(ProstheticStatus status, CancellationToken cancellationToken);
    Task<Option<ProstheticStatus>> SearchByName(string name, CancellationToken cancellationToken);
} 
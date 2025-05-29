using Domain.ProstheticStatuses;

namespace Application.Common.Interfaces.Queries;

public interface IProstheticStatusQueries
{
    Task<IReadOnlyList<ProstheticStatus>> GetAll(CancellationToken cancellationToken);
} 
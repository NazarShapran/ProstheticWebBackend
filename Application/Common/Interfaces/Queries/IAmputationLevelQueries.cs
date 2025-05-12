using Domain.AmputationLevels;

namespace Application.Common.Interfaces.Queries;

public interface IAmputationLevelQueries
{
    Task<IReadOnlyList<AmputationLevel>> GetAll(CancellationToken cancellationToken);
}
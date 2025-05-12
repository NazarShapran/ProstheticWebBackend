using Domain.AmputationLevels;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IAmputationLevelRepository
{
    Task<Option<AmputationLevel>> GetById(AmputationLevelId id, CancellationToken cancellationToken);
    Task<AmputationLevel> Add(AmputationLevel amputationLevel, CancellationToken cancellationToken);
    Task<Option<AmputationLevel>> SearchByTitle(string title, CancellationToken cancellationToken);
    Task<AmputationLevel> Update(AmputationLevel amputationLevel, CancellationToken cancellationToken);
    Task<AmputationLevel> Delete(AmputationLevel amputationLevel, CancellationToken cancellationToken);
}
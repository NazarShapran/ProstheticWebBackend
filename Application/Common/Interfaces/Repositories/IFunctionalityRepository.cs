using Optional;
using Domain.Functionalities;


public interface IFunctionalityRepository
{
    Task<Option<Functionality>> GetById(FunctionalityId id, CancellationToken cancellationToken);
    Task<Functionality> Add(Functionality functionality, CancellationToken cancellationToken);
    Task<Option<Functionality>> SearchByTitle(string title, CancellationToken cancellationToken);
    Task<Functionality> Update(Functionality functionality, CancellationToken cancellationToken);
    Task<Functionality> Delete(Functionality functionality, CancellationToken cancellationToken);
}
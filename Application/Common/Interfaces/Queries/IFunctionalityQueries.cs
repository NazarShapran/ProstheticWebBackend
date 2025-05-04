using Domain.Functionalitys;

namespace Application.Common.Interfaces.Queries;

public interface IFunctionalityQueries
{
    Task<IReadOnlyList<Functionality>> GetAll(CancellationToken cancellationToken);
}
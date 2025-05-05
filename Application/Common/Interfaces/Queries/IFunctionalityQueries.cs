namespace Application.Common.Interfaces.Queries;
using Domain.Functionalities;

public interface IFunctionalityQueries
{
    Task<IReadOnlyList<Functionality>> GetAll(CancellationToken cancellationToken);
}
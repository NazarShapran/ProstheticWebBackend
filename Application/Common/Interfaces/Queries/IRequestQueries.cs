using Domain.Request;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IRequestQueries
{
    Task<IReadOnlyList<Request>> GetAll(CancellationToken cancellationToken);
}   
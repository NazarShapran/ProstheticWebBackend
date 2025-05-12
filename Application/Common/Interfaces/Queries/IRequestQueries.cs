using Domain.Request;
using Domain.Users;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IRequestQueries
{
    Task<IReadOnlyList<Request>> GetAll(CancellationToken cancellationToken);
    Task<IReadOnlyList<Request>> SearchByUserId(UserId userId, CancellationToken cancellationToken);

    Task<Option<Request>> GetById(RequestId id, CancellationToken cancellationToken);
}   
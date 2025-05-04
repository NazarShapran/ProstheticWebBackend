using Domain.Request;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IRequestRepository
{
    Task<Request> Add(Request request, CancellationToken cancellationToken);
    Task<Request> Update(Request request, CancellationToken cancellationToken);
    Task<Request> Delete(Request request, CancellationToken cancellationToken);
    
    Task<Option<Request>> GetById(RequestId id, CancellationToken cancellationToken);
}
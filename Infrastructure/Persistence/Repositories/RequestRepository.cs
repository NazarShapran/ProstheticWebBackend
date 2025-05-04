using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Request;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class RequestRepository(ApplicationDbContext context) : IRequestRepository, IRequestQueries
{
    public async Task<IReadOnlyList<Request>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Requests
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Option<Request>> GetById(RequestId id, CancellationToken cancellationToken)
    {
        var entity = await context.Requests
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Request>() : Option.Some(entity);
    }

    public async Task<Request> Add(Request request, CancellationToken cancellationToken)
    {
        await context.Requests.AddAsync(request, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return request;
    }

    public async Task<Request> Update(Request request, CancellationToken cancellationToken)
    {
        context.Requests.Update(request);

        await context.SaveChangesAsync(cancellationToken);

        return request;
    }

    public async Task<Request> Delete(Request request, CancellationToken cancellationToken)
    {
        context.Requests.Remove(request);
        await context.SaveChangesAsync(cancellationToken);
        return request;
    }
}
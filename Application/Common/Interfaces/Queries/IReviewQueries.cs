using Domain.Reviews;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IReviewQueries
{
    Task<IReadOnlyList<Review>> GetAll(CancellationToken cancellationToken);
    Task<Option<Review>> GetById(ReviewId id, CancellationToken cancellationToken);
}
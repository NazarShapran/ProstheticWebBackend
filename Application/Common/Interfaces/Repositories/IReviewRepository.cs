using Domain.Reviews;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<Review> Add(Review review, CancellationToken cancellationToken);
    Task<Review> Update(Review review, CancellationToken cancellationToken);
    Task<Review> Delete(Review review, CancellationToken cancellationToken);
    
    Task<Option<Review>> GetById(ReviewId id, CancellationToken cancellationToken);
}
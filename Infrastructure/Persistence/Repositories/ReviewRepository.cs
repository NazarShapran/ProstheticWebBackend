using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Reviews;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ReviewRepository(ApplicationDbContext context): IReviewRepository, IReviewQueries
{
    public async Task<IReadOnlyList<Review>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Reviews
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Option<Review>> GetById(ReviewId id, CancellationToken cancellationToken)
    {
        var entity = await context.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Review>() : Option.Some(entity);
    }
    
    public async Task<Review> Add(Review review, CancellationToken cancellationToken)
    {
        await context.Reviews.AddAsync(review, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return review;
    }

    public async Task<Review> Update(Review review, CancellationToken cancellationToken)
    {
        context.Reviews.Update(review);

        await context.SaveChangesAsync(cancellationToken);

        return review;
    }
    public async Task<Review> Delete(Review review, CancellationToken cancellationToken)
    {
        context.Reviews.Remove(review);
        await context.SaveChangesAsync(cancellationToken);
        return review;
    }
}
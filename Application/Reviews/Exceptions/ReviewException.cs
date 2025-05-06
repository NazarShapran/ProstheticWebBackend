using Domain.Prosthetics;
using Domain.Reviews;
using Domain.Users;

namespace Application.Reviews.Exceptions;

public abstract class ReviewException(ReviewId reviewId, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ReviewId ReviewId { get; } = reviewId;
}
public class ReviewNotFoundException(ReviewId reviewId)  : ReviewException(reviewId, $"Review with ID {reviewId} not found.");

public class ReviewAlreadyExistsException(ReviewId reviewId) : ReviewException(reviewId, $"Review with ID {reviewId} already exists.");

public class ReviewUnknownException(ReviewId reviewId, Exception innerException) : ReviewException(reviewId, $"An unknown error occurred with review ID {reviewId}.", innerException);

public class ReviewUserNotFoundException(UserId userId) : ReviewException(ReviewId.Empty(), $"User with ID {userId} not found.");

public class ReviewProstheticNotFoundException(ProstheticId prostheticId) : ReviewException(ReviewId.Empty(), $"Prosthetic with ID {prostheticId} not found.");
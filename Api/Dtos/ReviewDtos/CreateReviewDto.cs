using Domain.Reviews;

namespace Api.Dtos.ReviewDtos;

public record CreateReviewDto(Guid? Id, string Description, string Pros, string Cons, DateTime Date, Guid UserId, Guid ProstheticId)
{
    public static CreateReviewDto FromDomainModel(Review review)
        => new(
            review.Id.Value,
            review.Description, 
            review.Pros, 
            review.Cons, 
            review.Date, 
            review.UserId.Value,
            review.ProstheticId.Value);
}
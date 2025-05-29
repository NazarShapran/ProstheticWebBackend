using Api.Dtos.UserDtos;
using Application.Reviews.Commands;
using Domain.Reviews;

namespace Api.Dtos.ReviewDtos;

public record ReviewDto(Guid? Id, string Description, string Pros, string Cons, DateTime Date, Guid UserId,UserDto? User, Guid ProstheticId)
{
    public static ReviewDto FromDomainModel(Review review)
        => new(
            review.Id.Value,
            review.Description, 
            review.Pros, 
            review.Cons, 
            review.Date, 
            review.UserId.Value,
            review.User != null ? UserDto.FromDomainModel(review.User) : null,
            review.ProstheticId.Value);
}
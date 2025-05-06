using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Reviews.Exceptions;
using Domain.Prosthetics;
using Domain.Reviews;
using Domain.Users;
using MediatR;

namespace Application.Reviews.Commands;

public record CreateReviewCommand : IRequest<Result<Review, ReviewException>>
{
    public required Guid ProstheticId { get; init; }
    public required Guid UserId { get; init; }
    
    public required string Description { get; init; }
    public required string Pros { get; init; }
    public required string Cons { get; init; }
}

public class CreateReviewCommandHandler(IReviewRepository reviewRepository,
    IProstheticRepository prostheticRepository, 
    IUserRepository userRepository) : IRequestHandler<CreateReviewCommand, Result<Review, ReviewException>>
{
    public async Task<Result<Review, ReviewException>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var prostheticId = new ProstheticId(request.ProstheticId);
        var existingProsthetic = await prostheticRepository.GetById(prostheticId, cancellationToken);
        
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match<Task<Result<Review, ReviewException>>>(async u =>
            {
                return await existingProsthetic.Match<Task<Result<Review, ReviewException>>>(async p => 
                        await CreateEntity(request.Description, request.Pros, request.Cons, userId, prostheticId, cancellationToken),
                    
                    () => Task.FromResult<Result<Review, ReviewException>>(
                        new ReviewProstheticNotFoundException(prostheticId)));
            },
            () => Task.FromResult<Result<Review, ReviewException>>(new ReviewUserNotFoundException(userId)));
    }

    private async Task<Result<Review, ReviewException>> CreateEntity(
        string requestDescription, 
        string requestPros, 
        string requestCons, 
        UserId userId, 
        ProstheticId prostheticId, 
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Review.New(ReviewId.New(), requestDescription, requestPros, requestCons, userId, prostheticId);
            return await reviewRepository.Add(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new ReviewUnknownException(ReviewId.Empty(), e);
        }
    }
}
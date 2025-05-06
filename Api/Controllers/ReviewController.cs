using Api.Dtos.ReviewDtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Reviews.Commands;
using Domain.Reviews;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("review")]
[ApiController]

public class ReviewController(ISender sender, IReviewQueries reviewQueries) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<ReviewDto>>> GetAll(CancellationToken cancellationToken)
    {
        var review = await reviewQueries.GetAll(cancellationToken);
        return review.Select(ReviewDto.FromDomainModel).ToList();
    }
    
    [HttpGet("get/{reviewId:guid}")]
    public async Task<ActionResult<ReviewDto>> Get([FromRoute] Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await reviewQueries.GetById(new ReviewId(reviewId), cancellationToken);
        return review.Match<ActionResult<ReviewDto>>(
            f => ReviewDto.FromDomainModel(f),
            () => NotFound()
        );
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<ReviewDto>> Create([FromBody] ReviewDto request, CancellationToken cancellationToken)
    {
        var input = new CreateReviewCommand
        {
            Description = request.Description,
            Pros = request.Pros,
            Cons = request.Cons,
            UserId = request.UserId,
            ProstheticId = request.ProstheticId
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<ReviewDto>>(
            f => ReviewDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }
}
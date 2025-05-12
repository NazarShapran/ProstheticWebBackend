using Api.Dtos.RequestDros;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Requests.Commands;
using Domain.Request;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("Request")]
[ApiController]
//[Authorize]

public class RequestController(ISender sender, IRequestQueries requestQueries) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<RequestDto>>> GetAll(CancellationToken cancellationToken)
    {
        var request = await requestQueries.GetAll(cancellationToken);
        return request.Select(RequestDto.FromDomainModel).ToList();
    }
    
    [HttpGet("get/{requestId:guid}")]
    public async Task<ActionResult<RequestDto>> Get([FromRoute] Guid requestId, CancellationToken cancellationToken)
    {
        var request = await requestQueries.GetById(new RequestId(requestId), cancellationToken);
        return request.Match<ActionResult<RequestDto>>(
            f => RequestDto.FromDomainModel(f),
            () => NotFound()
        );
    }
    
    [HttpGet("getByUserId/{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<RequestDto>>> GetByUserId([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var request = await requestQueries.SearchByUserId(new UserId(userId), cancellationToken);
        return request.Select(RequestDto.FromDomainModel).ToList();
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<RequestDto>> Create([FromBody] RequestDto request, CancellationToken cancellationToken)
    {
        var input = new CreateRequestCommand
        {
            Description = request.Description,
            UserId = request.UserId,
            ProstheticId = request.ProstheticId
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<RequestDto>>(
            f => RequestDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }
}
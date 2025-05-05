using Api.Dtos.TypeDtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.ProstheticTypes.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("prostheticType")]
[ApiController]

public class ProstheticTypeController(ISender sender, ITypeQueries prostheticTypeQueries) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<ProstheticTypeDto>>> GetAll(CancellationToken cancellationToken)
    {
        var functionalities = await prostheticTypeQueries.GetAll(cancellationToken);
        return functionalities.Select(ProstheticTypeDto.FromDomainModel).ToList();
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<ProstheticTypeDto>> Create([FromBody] ProstheticTypeDto request, CancellationToken cancellationToken)
    {
        var input = new CreateProstheticTypeCommand
        {
            Title = request.Title
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<ProstheticTypeDto>>(
            f => ProstheticTypeDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<ProstheticTypeDto>> Update([FromBody] ProstheticTypeDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateProstheticTypeCommand
        {
            ProstheticTypeId = request.Id!.Value,
            Title = request.Title
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<ProstheticTypeDto>>(
            f => ProstheticTypeDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
    
    [HttpDelete("delete/{prostheticTypeId:guid}")]
    public async Task<ActionResult<ProstheticTypeDto>> Delete([FromRoute] Guid prostheticTypeId, CancellationToken cancellationToken)
    {
        var input = new DeleteProstheticTypeCommand
        {
            ProstheticTypeId = prostheticTypeId
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<ProstheticTypeDto>>(
            f => ProstheticTypeDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
}
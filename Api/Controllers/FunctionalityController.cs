using Api.Dtos.FunctionalityDtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Functionalities.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("functionality")]
[ApiController]

public class FunctionalityController(ISender sender, IFunctionalityQueries functionalityQueries) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<FunctionalityDto>>> GetAll(CancellationToken cancellationToken)
    {
        var functionalities = await functionalityQueries.GetAll(cancellationToken);
        return functionalities.Select(FunctionalityDto.FromDomainModel).ToList();
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<FunctionalityDto>> Create([FromBody] FunctionalityDto request, CancellationToken cancellationToken)
    {
        var input = new CreateFunctionalityCommand
        {
            Title = request.Title
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<FunctionalityDto>>(
            f => FunctionalityDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<FunctionalityDto>> Update([FromBody] FunctionalityDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateFunctionalityCommand
        {
            FuncId = request.Id!.Value,
            Tittle = request.Title
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<FunctionalityDto>>(
            f => FunctionalityDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
    
    [HttpDelete("delete/{functionalityId:guid}")]
    public async Task<ActionResult<FunctionalityDto>> Delete([FromRoute] Guid functionalityId, CancellationToken cancellationToken)
    {
        var input = new DeleteFunctionalityCommand
        {
            FuncId = functionalityId
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<FunctionalityDto>>(
            f => FunctionalityDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
}
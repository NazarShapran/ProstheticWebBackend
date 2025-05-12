using Api.Dtos.AmputationLevelDtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.AmputationLevels.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("AmputationLevel")]
[ApiController]

public class AmputationLevelController(ISender sender, IAmputationLevelQueries amputationLevelQueries) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<AmputationLevelDto>>> GetAll(CancellationToken cancellationToken)
    {
        var functionalities = await amputationLevelQueries.GetAll(cancellationToken);
        return functionalities.Select(AmputationLevelDto.FromDomainModel).ToList();
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<AmputationLevelDto>> Create([FromBody] AmputationLevelDto request, CancellationToken cancellationToken)
    {
        var input = new CreateAmputationLevelCommand
        {
            Title = request.Title
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<AmputationLevelDto>>(
            f => AmputationLevelDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<AmputationLevelDto>> Update([FromBody] AmputationLevelDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateAmputationLevelCommand
        {
            AmputationLevelId = request.Id!.Value,
            Title = request.Title
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<AmputationLevelDto>>(
            f => AmputationLevelDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
    
    [HttpDelete("delete/{AmputationLevelId:guid}")]
    public async Task<ActionResult<AmputationLevelDto>> Delete([FromRoute] Guid amputationLevelId, CancellationToken cancellationToken)
    {
        var input = new DeleteAmputationLevelCommand
        {
            AmputationLevelId = amputationLevelId
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<AmputationLevelDto>>(
            f => AmputationLevelDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
}
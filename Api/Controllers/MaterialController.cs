using Api.Dtos.MaterialDtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Materials.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("material")]
[ApiController]

public class MaterialController(ISender sender, IMaterialQueries materialQueries) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<MaterialDto>>> GetAll(CancellationToken cancellationToken)
    {
        var functionalities = await materialQueries.GetAll(cancellationToken);
        return functionalities.Select(MaterialDto.FromDomainModel).ToList();
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<MaterialDto>> Create([FromBody] MaterialDto request, CancellationToken cancellationToken)
    {
        var input = new CreateMaterialCommand
        {
            Title = request.Title
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<MaterialDto>>(
            f => MaterialDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<MaterialDto>> Update([FromBody] MaterialDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateMaterialCommand
        {
            MaterialId = request.Id!.Value,
            Title = request.Title
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<MaterialDto>>(
            f => MaterialDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
    
    [HttpDelete("delete/{materialId:guid}")]
    public async Task<ActionResult<MaterialDto>> Delete([FromRoute] Guid materialId, CancellationToken cancellationToken)
    {
        var input = new DeleteMaterialCommand
        {
            MaterialId = materialId
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<MaterialDto>>(
            f => MaterialDto.FromDomainModel(f),
            e => e.ToObjectResult()
        );
    }
}
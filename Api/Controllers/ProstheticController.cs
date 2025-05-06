using Api.Dtos.ProstheticDtos;
using Application.Common.Interfaces.Queries;
using Domain.Prosthetics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("prosthetics")]
[ApiController]

public class ProstheticController(ISender sender, IProstheticQueries prostheticQueries) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<ProstheticDto>>> GetAll(CancellationToken cancellationToken)
    {
        var prosthetics = await prostheticQueries.GetAll(cancellationToken);
        return prosthetics.Select(ProstheticDto.FromDomainModel).ToList();
    }
    
    [HttpGet("get/{prostheticId:guid}")]
    public async Task<ActionResult<ProstheticDto>> Get([FromRoute] Guid prostheticId, CancellationToken cancellationToken)
    {
        var prosthetic = await prostheticQueries.GetById(new ProstheticId(prostheticId), cancellationToken);
        
        return prosthetic.Match<ActionResult<ProstheticDto>>(
            f => ProstheticDto.FromDomainModel(f),
            () => NotFound());
    }
}
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
    public async Task<ActionResult<PaginatedResult<ProstheticDto>>> GetAll(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 6,
        CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await prostheticQueries.GetAllPaged(page, pageSize, cancellationToken);

        var result = new PaginatedResult<ProstheticDto>
        {
            Items = items.Select(ProstheticDto.FromDomainModel).ToList(),
            TotalCount = totalCount
        };

        return Ok(result);
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
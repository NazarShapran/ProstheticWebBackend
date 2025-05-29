using Api.Dtos.ProstheticStatusDtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.ProstheticStatuses.Commands;
using Application.ProstheticStatuses.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("prosthetic-statuses")]
public class ProstheticStatusController(ISender sender, IProstheticStatusQueries statusQueries) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<ProstheticStatusDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await statusQueries.GetAll(cancellationToken);

        return result.Select(ProstheticStatusDto.FromDomainModel).ToList();
    }

    [HttpPost("create")]
    public async Task<ActionResult<ProstheticStatusDto>> Create([FromBody] ProstheticStatusDto dto, CancellationToken cancellationToken)
    {
        var input = new CreateProstheticStatusCommand
        {
            Title = dto.Title
        };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ProstheticStatusDto>>(
            status => ProstheticStatusDto.FromDomainModel(status),
            error => error.ToObjectResult()
            );
    }

    [HttpPut("update")]
    public async Task<ActionResult<ProstheticStatusDto>> Update([FromBody] ProstheticStatusDto dto, CancellationToken cancellationToken)
    {
        var input = new UpdateProstheticStatusCommand
        {
            Id = dto.Id!.Value,
            Title = dto.Title
        };
        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ProstheticStatusDto>>(
            status => ProstheticStatusDto.FromDomainModel(status),
            error => error.ToObjectResult()
        );
    }

    [HttpDelete("delete/{statusId:guid}")]
    public async Task<ActionResult<ProstheticStatusDto>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var input = new DeleteProstheticStatusCommand
        {
            Id = id
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<ProstheticStatusDto>>(
            status => ProstheticStatusDto.FromDomainModel(status),
            error => error switch
            {
                ProstheticStatusNotFoundException notFound => notFound.ToObjectResult(),
                _ => error.ToObjectResult()
            }
        );
    }
} 
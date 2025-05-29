using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProstheticStatuses.Exceptions;
using Domain.ProstheticStatuses;
using MediatR;

namespace Application.ProstheticStatuses.Commands;

public class UpdateProstheticStatusCommand : IRequest<Result<ProstheticStatus, ProstheticStatusException>>
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
}

public class UpdateProstheticStatusCommandHandler(IProstheticStatusRepository repository)
    : IRequestHandler<UpdateProstheticStatusCommand, Result<ProstheticStatus, ProstheticStatusException>>
{
    public async Task<Result<ProstheticStatus, ProstheticStatusException>> Handle(
        UpdateProstheticStatusCommand request,
        CancellationToken cancellationToken)
    {
        var statusId = new ProstheticStatusId(request.Id);
        var existingStatus = await repository.GetById(statusId, cancellationToken);

        return await existingStatus.Match(
            async s => await UpdateEntity(s, request.Title, cancellationToken),
            () => Task.FromResult<Result<ProstheticStatus, ProstheticStatusException>>(
                new ProstheticStatusNotFoundException(statusId)));
    }

    private async Task<Result<ProstheticStatus, ProstheticStatusException>> UpdateEntity(
        ProstheticStatus status,
        string title,
        CancellationToken cancellationToken)
    {
        try
        {
            status.ChangeTitle(title);
            return await repository.Update(status, cancellationToken);
        }
        catch (Exception e)
        {
            return new ProstheticStatusUnknownException(status.Id, e);
        }
    }
} 
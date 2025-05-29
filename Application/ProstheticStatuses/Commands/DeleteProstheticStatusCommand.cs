using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProstheticStatuses.Exceptions;
using Domain.ProstheticStatuses;
using MediatR;

namespace Application.ProstheticStatuses.Commands;

public class DeleteProstheticStatusCommand : IRequest<Result<ProstheticStatus, ProstheticStatusException>>
{
    public required Guid Id { get; init; }
}

public class DeleteProstheticStatusCommandHandler(IProstheticStatusRepository repository)
    : IRequestHandler<DeleteProstheticStatusCommand, Result<ProstheticStatus, ProstheticStatusException>>
{
    public async Task<Result<ProstheticStatus, ProstheticStatusException>> Handle(
        DeleteProstheticStatusCommand request,
        CancellationToken cancellationToken)
    {
        var statusId = new ProstheticStatusId(request.Id);
        var existingStatus = await repository.GetById(statusId, cancellationToken);

        return await existingStatus.Match(
            async s => await DeleteEntity(s, cancellationToken),
            () => Task.FromResult<Result<ProstheticStatus, ProstheticStatusException>>(
                new ProstheticStatusNotFoundException(statusId)));
    }

    private async Task<Result<ProstheticStatus, ProstheticStatusException>> DeleteEntity(
        ProstheticStatus status,
        CancellationToken cancellationToken)
    {
        try
        {
            return await repository.Delete(status, cancellationToken);
        }
        catch (Exception e)
        {
            return new ProstheticStatusUnknownException(status.Id, e);
        }
    }
} 
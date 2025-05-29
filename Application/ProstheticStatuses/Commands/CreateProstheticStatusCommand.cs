using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProstheticStatuses.Exceptions;
using Domain.ProstheticStatuses;
using MediatR;

namespace Application.ProstheticStatuses.Commands;

public class CreateProstheticStatusCommand : IRequest<Result<ProstheticStatus, ProstheticStatusException>>
{
    public required string Title { get; init; }
}

public class CreateProstheticStatusCommandHandler(IProstheticStatusRepository repository)
    : IRequestHandler<CreateProstheticStatusCommand, Result<ProstheticStatus, ProstheticStatusException>>
{
    public async Task<Result<ProstheticStatus, ProstheticStatusException>> Handle(
        CreateProstheticStatusCommand request,
        CancellationToken cancellationToken)
    {
        var existingStatus = await repository.SearchByName(request.Title, cancellationToken);

        return await existingStatus.Match(
            s => Task.FromResult<Result<ProstheticStatus, ProstheticStatusException>>(
                new ProstheticStatusAlreadyExistsException(s.Id)),
            async () => await CreateEntity(request.Title, cancellationToken));
    }

    private async Task<Result<ProstheticStatus, ProstheticStatusException>> CreateEntity(
        string title,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = ProstheticStatus.New(ProstheticStatusId.New(), title);
            return await repository.Create(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new ProstheticStatusUnknownException(ProstheticStatusId.Empty(), e);
        }
    }
} 
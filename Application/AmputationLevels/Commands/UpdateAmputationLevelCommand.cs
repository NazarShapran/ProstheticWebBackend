using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.AmputationLevels.Exceptions;
using Domain.AmputationLevels;
using MediatR;
using Optional;

namespace Application.AmputationLevels.Commands;

public record UpdateAmputationLevelCommand : IRequest<Result<AmputationLevel, AmputationLevelException>>
{
    public required Guid AmputationLevelId { get; init; }
    public required string Title { get; init; }
}

public class UpdateAmputationLevelCommandHandler(IAmputationLevelRepository amputationLevelRepository) 
    : IRequestHandler<UpdateAmputationLevelCommand, Result<AmputationLevel, AmputationLevelException>>
{
    public async Task<Result<AmputationLevel, AmputationLevelException>> Handle(
        UpdateAmputationLevelCommand request, CancellationToken cancellationToken)
    {
        var AmputationLevelId = new AmputationLevelId(request.AmputationLevelId);
        var AmputationLevel = await amputationLevelRepository.GetById(AmputationLevelId, cancellationToken);

        return await AmputationLevel.Match(
            async f =>
        {
            var existingFunc = await CheckDuplicated(AmputationLevelId, request.Title, cancellationToken);
            
            return await existingFunc.Match(
                d => Task.FromResult<Result<AmputationLevel, AmputationLevelException>>(new AmputationLevelAlreadyExistsException(d.Id)),
                async () => await UpdateEntity(f, request.Title, cancellationToken));
        },
        () => Task.FromResult<Result<AmputationLevel, AmputationLevelException>>(new AmputationLevelNotFoundException(AmputationLevelId)));
    }


    private async Task<Result<AmputationLevel, AmputationLevelException>> UpdateEntity(
        AmputationLevel AmputationLevel, 
        string title, 
        CancellationToken cancellationToken)
    {
        try
        {
            AmputationLevel.UpdateDetails(title);

            return await amputationLevelRepository.Update(AmputationLevel, cancellationToken);
        }
        catch (Exception exception)
        {
            return new AmputationLevelUnknownException(AmputationLevel.Id, exception);
        }
    }
    private async Task<Option<AmputationLevel>> CheckDuplicated(AmputationLevelId funcId, 
        string title,
        CancellationToken cancellationToken)
    {
        var func = await amputationLevelRepository.SearchByTitle(title, cancellationToken);
        return func.Match(
            f => f.Id == funcId ? Option.None<AmputationLevel>() : Option.Some(f),
            Option.None<AmputationLevel>);
    }
}
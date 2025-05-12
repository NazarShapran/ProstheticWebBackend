using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.AmputationLevels.Exceptions;
using Domain.AmputationLevels;
using MediatR;

namespace Application.AmputationLevels.Commands;

public record DeleteAmputationLevelCommand : IRequest<Result<AmputationLevel, AmputationLevelException>>
{
    public required Guid AmputationLevelId { get; init; }
}
public class DeleteAmputationLevelCommandHandler(IAmputationLevelRepository amputationLevelRepository) 
    : IRequestHandler<DeleteAmputationLevelCommand, Result<AmputationLevel, AmputationLevelException>>
{
    public async Task<Result<AmputationLevel, AmputationLevelException>> Handle(
        DeleteAmputationLevelCommand request, CancellationToken cancellationToken)
    {
        var AmputationLevelId = new AmputationLevelId(request.AmputationLevelId);
        
        var existingAmputationLevel = await amputationLevelRepository.GetById(AmputationLevelId, cancellationToken);

        return await existingAmputationLevel.Match<Task<Result<AmputationLevel, AmputationLevelException>>>(
            async f => await DeleteEntity(f, cancellationToken),
            () => Task.FromResult<Result<AmputationLevel, AmputationLevelException>>(new AmputationLevelNotFoundException(AmputationLevelId)));
    }
    private async Task<Result<AmputationLevel, AmputationLevelException>> DeleteEntity(
        AmputationLevel AmputationLevel, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await amputationLevelRepository.Delete(AmputationLevel, cancellationToken);
        }
        catch (Exception exception)
        {
            return new AmputationLevelUnknownException(AmputationLevel.Id, exception);
        }
    }
}
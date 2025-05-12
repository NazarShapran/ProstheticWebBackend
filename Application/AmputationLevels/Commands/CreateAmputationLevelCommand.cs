using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.AmputationLevels.Exceptions;
using Domain.AmputationLevels;
using MediatR;

namespace Application.AmputationLevels.Commands;


public record CreateAmputationLevelCommand : IRequest<Result<AmputationLevel, AmputationLevelException>>
{
    public required string Title { get; init; }
} 
public class CreateAmputationLevelCommandHandler(IAmputationLevelRepository amputationLevelRepository) 
    : IRequestHandler<CreateAmputationLevelCommand, Result<AmputationLevel, AmputationLevelException>>
{
    public async Task<Result<AmputationLevel, AmputationLevelException>> Handle(CreateAmputationLevelCommand request, CancellationToken cancellationToken)
    {
        var existingAmputationLevel = await amputationLevelRepository.SearchByTitle(request.Title, cancellationToken);
        
        return await existingAmputationLevel.Match(
            f => Task.FromResult<Result<AmputationLevel, AmputationLevelException>>(new AmputationLevelAlreadyExistsException(f.Id)),
            async () => await CreateEntity(request.Title, cancellationToken));
    }

    private async Task<Result<AmputationLevel, AmputationLevelException>> CreateEntity(
        string requestTitle, CancellationToken cancellationToken)
    {
        try
        {
            var entity = AmputationLevel.New(AmputationLevelId.New(), requestTitle);
            
            return await amputationLevelRepository.Add(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new AmputationLevelUnknownException(AmputationLevelId.Empty, e);
        }
    }
}
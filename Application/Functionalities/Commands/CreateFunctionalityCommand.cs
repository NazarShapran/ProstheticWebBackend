using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Functionalities.Exceptions;
using Domain.Functionalities;
using MediatR;

namespace Application.Functionalities.Commands;


public record CreateFunctionalityCommand : IRequest<Result<Functionality, FunctionalityException>>
{
    public required string Title { get; init; }
}
public class CreateFunctionalityCommandHandler(IFunctionalityRepository functionalityRepository) 
    : IRequestHandler<CreateFunctionalityCommand, Result<Functionality, FunctionalityException>>
{
    public async Task<Result<Functionality, FunctionalityException>> Handle(CreateFunctionalityCommand request, CancellationToken cancellationToken)
    {
        var existingFunc = await functionalityRepository.SearchByTitle(request.Title, cancellationToken);
        
        return await existingFunc.Match(
            f => Task.FromResult<Result<Functionality, FunctionalityException>>(new FunctionalityAlreadyExistsException(f.Id)),
            async () => await CreateEntity(request.Title, cancellationToken));
    }

    private async Task<Result<Functionality, FunctionalityException>> CreateEntity(
        string requestTitle, CancellationToken cancellationToken)
    {
        try
        {
            var entity = Functionality.New(FunctionalityId.New(), requestTitle);
            
            return await functionalityRepository.Add(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new FunctionalityUnknownException(FunctionalityId.Empty, e);
        }
    }
}
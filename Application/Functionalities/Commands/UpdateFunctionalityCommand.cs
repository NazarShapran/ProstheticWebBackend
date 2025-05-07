using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Functionalities.Exceptions;
using Domain.Functionalities;
using MediatR;
using Optional;

namespace Application.Functionalities.Commands;

public record UpdateFunctionalityCommand : IRequest<Result<Functionality, FunctionalityException>>
{
    public required Guid FuncId { get; init; }
    public required string Tittle { get; init; }
}

public class UpdateFunctionalityCommandHandler(IFunctionalityRepository functionalityRepository) 
    : IRequestHandler<UpdateFunctionalityCommand, Result<Functionality, FunctionalityException>>
{
    public async Task<Result<Functionality, FunctionalityException>> Handle(
        UpdateFunctionalityCommand request, CancellationToken cancellationToken)
    {
        var funcId = new FunctionalityId(request.FuncId);
        var func = await functionalityRepository.GetById(funcId, cancellationToken);

        return await func.Match(
            async f =>
        {
            var existingFunc = await CheckDuplicated(funcId, request.Tittle, cancellationToken);
            
            return await existingFunc.Match(
                d => Task.FromResult<Result<Functionality, FunctionalityException>>(new FunctionalityAlreadyExistsException(d.Id)),
                async () => await UpdateEntity(f, request.Tittle, cancellationToken));
        },
        () => Task.FromResult<Result<Functionality, FunctionalityException>>(new FunctionalityNotFoundException(funcId)));
    }


    private async Task<Result<Functionality, FunctionalityException>> UpdateEntity(
        Functionality functionality, 
        string tittle, 
        CancellationToken cancellationToken)
    {
        try
        {
            functionality.UpdateDetails(tittle);

            return await functionalityRepository.Update(functionality, cancellationToken);
        }
        catch (Exception exception)
        {
            return new FunctionalityUnknownException(functionality.Id, exception);
        }
    }
    private async Task<Option<Functionality>> CheckDuplicated(FunctionalityId funcId, 
        string title,
        CancellationToken cancellationToken)
    {
        var func = await functionalityRepository.SearchByTitle(title, cancellationToken);
        return func.Match(
            f => f.Id == funcId ? Option.None<Functionality>() : Option.Some(f),
            Option.None<Functionality>);
    }
}
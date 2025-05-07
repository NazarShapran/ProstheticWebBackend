using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Functionalities.Exceptions;
using Domain.Functionalities;
using MediatR;

namespace Application.Functionalities.Commands;

public record DeleteFunctionalityCommand : IRequest<Result<Functionality, FunctionalityException>>
{
    public required Guid FuncId { get; init; }
}
public class DeleteFunctionalityCommandHandler(IFunctionalityRepository functionalityRepository) 
    : IRequestHandler<DeleteFunctionalityCommand, Result<Functionality, FunctionalityException>>
{
    public async Task<Result<Functionality, FunctionalityException>> Handle(
        DeleteFunctionalityCommand request, CancellationToken cancellationToken)
    {
        var funcId = new FunctionalityId(request.FuncId);
        
        var existingFunctionality = await functionalityRepository.GetById(funcId, cancellationToken);

        return await existingFunctionality.Match<Task<Result<Functionality, FunctionalityException>>>(
            async f => await DeleteEntity(f, cancellationToken),
            () => Task.FromResult<Result<Functionality, FunctionalityException>>(new FunctionalityNotFoundException(funcId)));
    }
    private async Task<Result<Functionality, FunctionalityException>> DeleteEntity(
        Functionality functionality, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await functionalityRepository.Delete(functionality, cancellationToken);
        }
        catch (Exception exception)
        {
            return new FunctionalityUnknownException(functionality.Id, exception);
        }
    }
}
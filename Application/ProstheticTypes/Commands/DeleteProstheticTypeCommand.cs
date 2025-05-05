using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProstheticTypes.Exceptions;
using Domain.ProstheticTypes;
using MediatR;

namespace Application.ProstheticTypes.Commands;

public record DeleteProstheticTypeCommand : IRequest<Result<ProstheticType, ProstheticTypeException>>
{
    public required Guid ProstheticTypeId { get; init; }
}
public class DeleteProstheticTypeCommandHandler(ITypeRepository prostheticTypeRepository) 
    : IRequestHandler<DeleteProstheticTypeCommand, Result<ProstheticType, ProstheticTypeException>>
{
    public async Task<Result<ProstheticType, ProstheticTypeException>> Handle(
        DeleteProstheticTypeCommand request, CancellationToken cancellationToken)
    {
        var prostheticTypeId = new TypeId(request.ProstheticTypeId);
        
        var existingProstheticType = await prostheticTypeRepository.GetById(prostheticTypeId, cancellationToken);

        return await existingProstheticType.Match<Task<Result<ProstheticType, ProstheticTypeException>>>(
            async f => await DeleteEntity(f, cancellationToken),
            () => Task.FromResult<Result<ProstheticType, ProstheticTypeException>>(new ProstheticTypeNotFoundException(prostheticTypeId)));
    }
    private async Task<Result<ProstheticType, ProstheticTypeException>> DeleteEntity(
        ProstheticType prostheticType, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await prostheticTypeRepository.Delete(prostheticType, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProstheticTypeUnknownException(prostheticType.Id, exception);
        }
    }
}
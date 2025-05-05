using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProstheticTypes.Exceptions;
using Domain.ProstheticTypes;
using MediatR;

namespace Application.ProstheticTypes.Commands;

public record CreateProstheticTypeCommand : IRequest<Result<ProstheticType, ProstheticTypeException>>
{
    public required string Title { get; init; }
} 
public class CreateProstheticTypeCommandHandler(ITypeRepository prostheticTypeRepository) 
    : IRequestHandler<CreateProstheticTypeCommand, Result<ProstheticType, ProstheticTypeException>>
{
    public async Task<Result<ProstheticType, ProstheticTypeException>> Handle(CreateProstheticTypeCommand request, CancellationToken cancellationToken)
    {
        var existingProstheticType = await prostheticTypeRepository.SearchByTitle(request.Title, cancellationToken);
        
        return await existingProstheticType.Match(
            f => Task.FromResult<Result<ProstheticType, ProstheticTypeException>>(new ProstheticTypeAlreadyExistsException(f.Id)),
            async () => await CreateEntity(request.Title, cancellationToken));
    }

    private async Task<Result<ProstheticType, ProstheticTypeException>> CreateEntity(
        string requestTitle, CancellationToken cancellationToken)
    {
        try
        {
            var entity = ProstheticType.New(TypeId.New(), requestTitle);
            
            return await prostheticTypeRepository.Add(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new ProstheticTypeUnknownException(TypeId.Empty, e);
        }
    }
}
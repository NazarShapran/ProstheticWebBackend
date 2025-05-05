using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ProstheticTypes.Exceptions;
using Domain.ProstheticTypes;
using MediatR;
using Optional;

namespace Application.ProstheticTypes.Commands;

public record UpdateProstheticTypeCommand : IRequest<Result<ProstheticType, ProstheticTypeException>>
{
    public required Guid ProstheticTypeId { get; init; }
    public required string Title { get; init; }
}

public class UpdateProstheticTypeCommandHandler(ITypeRepository prostheticTypeRepository) 
    : IRequestHandler<UpdateProstheticTypeCommand, Result<ProstheticType, ProstheticTypeException>>
{
    public async Task<Result<ProstheticType, ProstheticTypeException>> Handle(
        UpdateProstheticTypeCommand request, CancellationToken cancellationToken)
    {
        var prostheticTypeId = new TypeId(request.ProstheticTypeId);
        var prostheticType = await prostheticTypeRepository.GetById(prostheticTypeId, cancellationToken);

        return await prostheticType.Match(
            async f =>
        {
            var existingFunc = await CheckDuplicated(prostheticTypeId, request.Title, cancellationToken);
            
            return await existingFunc.Match(
                d => Task.FromResult<Result<ProstheticType, ProstheticTypeException>>(new ProstheticTypeAlreadyExistsException(d.Id)),
                async () => await UpdateEntity(f, request.Title, cancellationToken));
        },
        () => Task.FromResult<Result<ProstheticType, ProstheticTypeException>>(new ProstheticTypeNotFoundException(prostheticTypeId)));
    }


    private async Task<Result<ProstheticType, ProstheticTypeException>> UpdateEntity(
        ProstheticType prostheticType, 
        string title, 
        CancellationToken cancellationToken)
    {
        try
        {
            prostheticType.UpdateDetails(title);

            return await prostheticTypeRepository.Update(prostheticType, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProstheticTypeUnknownException(prostheticType.Id, exception);
        }
    }
    private async Task<Option<ProstheticType>> CheckDuplicated(TypeId funcId, 
        string title,
        CancellationToken cancellationToken)
    {
        var func = await prostheticTypeRepository.SearchByTitle(title, cancellationToken);
        return func.Match(
            f => f.Id == funcId ? Option.None<ProstheticType>() : Option.Some(f),
            Option.None<ProstheticType>);
    }
}
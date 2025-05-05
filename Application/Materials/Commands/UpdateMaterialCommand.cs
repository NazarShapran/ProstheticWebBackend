using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Materials.Exceptions;
using Domain.Materials;
using MediatR;
using Optional;

namespace Application.Materials.Commands;

public record UpdateMaterialCommand : IRequest<Result<Material, MaterialException>>
{
    public required Guid MaterialId { get; init; }
    public required string Title { get; init; }
}

public class UpdateMaterialCommandHandler(IMaterialRepository materialRepository) 
    : IRequestHandler<UpdateMaterialCommand, Result<Material, MaterialException>>
{
    public async Task<Result<Material, MaterialException>> Handle(
        UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var materialId = new MaterialId(request.MaterialId);
        var material = await materialRepository.GetById(materialId, cancellationToken);

        return await material.Match(
            async f =>
        {
            var existingFunc = await CheckDuplicated(materialId, request.Title, cancellationToken);
            
            return await existingFunc.Match(
                d => Task.FromResult<Result<Material, MaterialException>>(new MaterialAlreadyExistsException(d.Id)),
                async () => await UpdateEntity(f, request.Title, cancellationToken));
        },
        () => Task.FromResult<Result<Material, MaterialException>>(new MaterialNotFoundException(materialId)));
    }


    private async Task<Result<Material, MaterialException>> UpdateEntity(
        Material material, 
        string title, 
        CancellationToken cancellationToken)
    {
        try
        {
            material.UpdateDetails(title);

            return await materialRepository.Update(material, cancellationToken);
        }
        catch (Exception exception)
        {
            return new MaterialUnknownException(material.Id, exception);
        }
    }
    private async Task<Option<Material>> CheckDuplicated(MaterialId funcId, 
        string title,
        CancellationToken cancellationToken)
    {
        var func = await materialRepository.SearchByTitle(title, cancellationToken);
        return func.Match(
            f => f.Id == funcId ? Option.None<Material>() : Option.Some(f),
            Option.None<Material>);
    }
}
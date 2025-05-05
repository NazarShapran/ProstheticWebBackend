using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Materials.Exceptions;
using Domain.Materials;
using MediatR;

namespace Application.Materials.Commands;

public record DeleteMaterialCommand : IRequest<Result<Material, MaterialException>>
{
    public required Guid MaterialId { get; init; }
}
public class DeleteMaterialCommandHandler(IMaterialRepository materialRepository) 
    : IRequestHandler<DeleteMaterialCommand, Result<Material, MaterialException>>
{
    public async Task<Result<Material, MaterialException>> Handle(
        DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        var materialId = new MaterialId(request.MaterialId);
        
        var existingMaterial = await materialRepository.GetById(materialId, cancellationToken);

        return await existingMaterial.Match<Task<Result<Material, MaterialException>>>(
            async f => await DeleteEntity(f, cancellationToken),
            () => Task.FromResult<Result<Material, MaterialException>>(new MaterialNotFoundException(materialId)));
    }
    private async Task<Result<Material, MaterialException>> DeleteEntity(
        Material material, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await materialRepository.Delete(material, cancellationToken);
        }
        catch (Exception exception)
        {
            return new MaterialUnknownException(material.Id, exception);
        }
    }
}
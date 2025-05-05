using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Materials.Exceptions;
using Domain.Materials;
using MediatR;

namespace Application.Materials.Commands;


public record CreateMaterialCommand : IRequest<Result<Material, MaterialException>>
{
    public required string Title { get; init; }
} 
public class CreateMaterialCommandHandler(IMaterialRepository materialRepository) 
    : IRequestHandler<CreateMaterialCommand, Result<Material, MaterialException>>
{
    public async Task<Result<Material, MaterialException>> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var existingMaterial = await materialRepository.SearchByTitle(request.Title, cancellationToken);
        
        return await existingMaterial.Match(
            f => Task.FromResult<Result<Material, MaterialException>>(new MaterialAlreadyExistsException(f.Id)),
            async () => await CreateEntity(request.Title, cancellationToken));
    }

    private async Task<Result<Material, MaterialException>> CreateEntity(
        string requestTitle, CancellationToken cancellationToken)
    {
        try
        {
            var entity = Material.New(MaterialId.New(), requestTitle);
            
            return await materialRepository.Add(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new MaterialUnknownException(MaterialId.Empty, e);
        }
    }
}
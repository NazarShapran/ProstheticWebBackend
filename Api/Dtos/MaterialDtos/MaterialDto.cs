using Domain.Materials;

namespace Api.Dtos.MaterialDtos;

public record MaterialDto(Guid? Id, string Title)
{
    public static MaterialDto FromDomainModel(Material material)
    => new(material.Id.Value, material.Title);
}
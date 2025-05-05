using Domain.ProstheticTypes;

namespace Api.Dtos.TypeDtos;

public record ProstheticTypeDto(Guid? Id, string Title)
{
    public static ProstheticTypeDto FromDomainModel(ProstheticType type)
    => new(type.Id.Value, type.Title);
}
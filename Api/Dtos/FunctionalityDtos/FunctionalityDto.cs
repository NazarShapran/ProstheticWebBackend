using Domain.Functionalities;

namespace Api.Dtos.FunctionalityDtos;

public record FunctionalityDto(Guid? Id, string Title)
{
    public static FunctionalityDto FromDomainModel(Functionality functionality)
    => new(functionality.Id.Value, functionality.Title);
}
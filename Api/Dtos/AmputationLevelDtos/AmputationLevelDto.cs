using Domain.AmputationLevels;

namespace Api.Dtos.AmputationLevelDtos;

public record AmputationLevelDto(Guid? Id, string Title)
{
    public static AmputationLevelDto FromDomainModel(AmputationLevel amputationLevel)
    => new(amputationLevel.Id.Value, amputationLevel.Title);
}
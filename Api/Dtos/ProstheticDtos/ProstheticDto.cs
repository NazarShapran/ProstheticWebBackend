using Api.Dtos.AmputationLevelDtos;
using Api.Dtos.FunctionalityDtos;
using Api.Dtos.MaterialDtos;
using Api.Dtos.ProstheticStatusDtos;
using Api.Dtos.TypeDtos;

namespace Api.Dtos.ProstheticDtos;

public record ProstheticDto(
    Guid? Id,
    string Title,
    string Description,
    double Weight,
    Guid TypeId,
    ProstheticTypeDto? Type,
    Guid MaterialId,
    MaterialDto? Material,
    Guid FunctionalityId,
    FunctionalityDto? Functionality,
    Guid? AmputationLevelId,
    AmputationLevelDto? AmputationLevel,
    Guid StatusId,
    ProstheticStatusDto? Status
    )
{
    public static ProstheticDto FromDomainModel(Domain.Prosthetics.Prosthetic prosthetic)
        => new(
            prosthetic.Id.Value,
            prosthetic.Title,
            prosthetic.Description,
            prosthetic.Weight,
            prosthetic.TypeId.Value,
            prosthetic.Type == null ? null : ProstheticTypeDto.FromDomainModel(prosthetic.Type),
            prosthetic.MaterialId.Value,
            prosthetic.Material == null ? null : MaterialDto.FromDomainModel(prosthetic.Material),
            prosthetic.FunctionalityId.Value,
            prosthetic.Functionality == null ? null : FunctionalityDto.FromDomainModel(prosthetic.Functionality),
            prosthetic.AmputationLevelId?.Value,
            prosthetic.AmputationLevel == null ? null : AmputationLevelDto.FromDomainModel(prosthetic.AmputationLevel),
            prosthetic.StatusId.Value,
            prosthetic.Status == null ? null : ProstheticStatusDto.FromDomainModel(prosthetic.Status)
        );
}
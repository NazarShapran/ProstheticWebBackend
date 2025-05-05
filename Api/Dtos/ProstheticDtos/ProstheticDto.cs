namespace Api.Dtos.ProstheticDtos;

public record ProstheticDto(
    Guid? Id,
    string Title,
    string Description,
    double Weight,
    Guid TypeId,
    Guid MaterialId,
    Guid FunctionalityId)
{
    public static ProstheticDto FromDomainModel(Domain.Prosthetics.Prosthetic prosthetic)
        => new(
            prosthetic.Id.Value,
            prosthetic.Title,
            prosthetic.Description,
            prosthetic.Weight,
            prosthetic.TypeId.Value,
            prosthetic.MaterialId.Value,
            prosthetic.FunctionalityId.Value
        );
}
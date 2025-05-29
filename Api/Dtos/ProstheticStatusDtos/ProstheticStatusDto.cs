using Domain.ProstheticStatuses;

namespace Api.Dtos.ProstheticStatusDtos;

public record ProstheticStatusDto(Guid? Id, string Title)
{
    public static ProstheticStatusDto FromDomainModel(ProstheticStatus status)
        => new(status.Id.Value, status.Title);
} 
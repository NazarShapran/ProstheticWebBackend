using Api.Dtos.ProstheticDtos;
using Api.Dtos.StatusDtos;
using Domain.Request;

namespace Api.Dtos.RequestDros;

public record RequestDto(Guid? Id, string Description, Guid UserId, Guid ProstheticId, ProstheticDto? Prosthetic, Guid StatusId, StatusDto? Status)
{
    public static RequestDto FromDomainModel(Request request)
        => new(
            request.Id.Value,
            request.Description,
            request.UserId.Value,
            request.ProstheticId.Value,
            request.Prosthetic == null ? null : ProstheticDto.FromDomainModel(request.Prosthetic),
            request.StatusId.Value, 
            request.Status == null ? null : StatusDto.FromDomainModel(request.Status)
        );
}
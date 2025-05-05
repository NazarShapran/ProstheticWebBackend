using Domain.Request;

namespace Api.Dtos.RequestDros;

public record ChangeStatusRequestDto(
    Guid Id,
    Guid StatusId)
{
    public static ChangeStatusRequestDto FromDomainModel(Request request)
        => new ChangeStatusRequestDto(request.Id.Value, request.StatusId.Value);
}
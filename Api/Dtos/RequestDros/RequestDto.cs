using Domain.Request;

namespace Api.Dtos.RequestDros;

public record RequestDto(Guid? Id, string Description, Guid UserId, Guid ProstheticId, Guid StatusId)
{
    public static RequestDto FromDomainModel(Request request)
        => new(
            request.Id.Value,
            request.Description,
            request.UserId.Value,
            request.ProstheticId.Value,
            request.StatusId.Value
        );
}
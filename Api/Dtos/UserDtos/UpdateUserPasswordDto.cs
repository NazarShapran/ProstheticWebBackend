using Domain.Users;

namespace Api.Dtos.UserDtos;

public record UpdateUserPasswordDto(Guid UserId, string Password)
{
    public static UpdateUserPasswordDto FromModelDomain(User user)
    => new(
        UserId: user.Id.Value,
        Password: user.Password
        );
}
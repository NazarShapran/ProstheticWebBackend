using Domain.Users;

namespace Api.Dtos.UserDtos;

public record LoginUserDto(string Email, string Password)
{
    public static LoginUserDto FromModelDomain(User user)
        => new(Email: user.Email, Password: user.Password);
}
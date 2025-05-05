using Domain.Users;

namespace Api.Dtos.UserDtos;

public record CreateUserDto
(
    string FullName,
    string PhoneNumber,
    DateTime BirthDate,
    string Email,
    string Password)
{
    public static CreateUserDto FromUser(User user)
        => new(
            FullName: user.FullName,
            PhoneNumber: user.PhoneNumber,
            BirthDate: user.BirthDate,
            Email: user.Email,
            Password: user.Password);
}
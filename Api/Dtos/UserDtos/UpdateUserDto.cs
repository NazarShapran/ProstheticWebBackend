using Domain.Users;

namespace Api.Dtos.UserDtos;

public record UpdateUserDto(
    Guid? UserId,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime BirthDate)
{
    public static UpdateUserDto FromDomainModel(User user)
        => new
        (
            UserId: user.Id.Value,
            FullName: user.FullName,
            Email: user.Email,
            PhoneNumber: user.PhoneNumber,
            BirthDate: user.BirthDate
        );
}
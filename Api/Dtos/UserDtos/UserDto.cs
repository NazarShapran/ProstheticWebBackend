using Domain.Users;

namespace Api.Dtos.UserDtos;

public record UserDto(Guid? Id, string FullName, string Email, string PhoneNumber, DateTime BirthDate, Guid RoleId)
{
    public static UserDto FromDomainModel(User user)
        => new(user.Id.Value, user.FullName, user.Email, user.PhoneNumber, user.BirthDate, user.RoleId.Value);
}
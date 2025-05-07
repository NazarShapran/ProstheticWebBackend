using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Users.Exceptions;
using Domain.Roles;
using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public class RegisterUserCommand : IRequest<Result<User, UserException>>
{
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string PhoneNumber { get; init; }
    public DateTime BirthDate { get; init; }
}

public class RegisterUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    : IRequestHandler<RegisterUserCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var userRole = "User";
        var exsitingRole = await roleRepository.SearchByName(userRole, cancellationToken);
        var exsitingUser = await userRepository.GetByEmail(request.Email, cancellationToken);

        return await exsitingUser.Match(
            u => Task.FromResult<Result<User, UserException>>(new UserAlreadyExistsException(u.Id)),
            async () =>
            {
                return await exsitingRole.Match(
                    async r => await CreateEntity(UserId.New(), request.FullName, request.Password, 
                        request.Email,  r.Id, request.PhoneNumber, request.BirthDate, cancellationToken),
                    () => Task.FromResult<Result<User, UserException>>(new RoleNotFound(UserId.Empty(),
                        RoleId.Empty())));
            });
    }

    private async Task<Result<User, UserException>> CreateEntity(
        UserId userId,
        string fullName,
        string password,
        string email,
        RoleId roleId,
        string phoneNumber,
        DateTime birthDate,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = User.New(userId, fullName, email, password, roleId, phoneNumber, birthDate);

            return await userRepository.Create(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new UserUnknownException(UserId.Empty(), e);
        }
    }
}
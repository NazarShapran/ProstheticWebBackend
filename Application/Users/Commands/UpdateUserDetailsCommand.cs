using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Users.Exceptions;
using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public class UpdateUserDetailsCommand : IRequest<Result<User, UserException>>
{
    public Guid UserId { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public DateTime BirthDate { get; init; }
}

public class UpdateUserDetailsCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateUserDetailsCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(UpdateUserDetailsCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match(
            async u => await UpdateEntity(u, request.FullName, request.Email,  request.BirthDate, request.PhoneNumber, cancellationToken),
            () => Task.FromResult<Result<User, UserException>>(new UserNotFoundException(userId))
        );
    }

    private async Task<Result<User, UserException>> UpdateEntity(
        User user, 
        string fullName, 
        string email, 
        DateTime birthDate, 
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        try 
        {
            user.UpdateDetails(fullName, email, phoneNumber,birthDate);
            return await userRepository.Update(user, cancellationToken);
        }
        catch (Exception e)
        {
            return new UserUnknownException(user.Id, e);
        }
    }
} 
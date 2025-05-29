using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Users.Exceptions;
using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public class UpdateUserPasswordCommand : IRequest<Result<User, UserException>>
{
    public Guid UserId { get; init; }
    public required string Password { get; init; }
}

public class UpdateUserPasswordCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateUserPasswordCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(UpdateUserPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match(
            async u => await UpdatePassword(u, request.Password, cancellationToken),
            () => Task.FromResult<Result<User, UserException>>(new UserNotFoundException(userId))
        );
    }

    private async Task<Result<User, UserException>> UpdatePassword(
        User user, 
        string password, 
        CancellationToken cancellationToken)
    {
        try
        {
            user.UpdatePassword(password);
            return await userRepository.Update(user, cancellationToken);
        }
        catch (Exception e)
        {
            return new UserUnknownException(user.Id, e);
        }
    }
} 
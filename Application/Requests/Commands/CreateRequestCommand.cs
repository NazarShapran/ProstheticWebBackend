using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Requests.Exceptions;
using Domain.Prosthetics;
using Domain.Request;
using Domain.Users;
using MediatR;

namespace Application.Requests.Commands;

public record CreateRequestCommand : IRequest<Result<Request, RequestException>>
{
    public required Guid ProstheticId { get; init; }
    public required Guid UserId { get; init; }
    
    public required string Description { get; init; }
}

public class CreateRequestCommandHandler(IRequestRepository requestRepository,
    IProstheticRepository prostheticRepository, 
    IUserRepository userRepository) : IRequestHandler<CreateRequestCommand, Result<Request, RequestException>>
{
    public async Task<Result<Request, RequestException>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        var prostheticId = new ProstheticId(request.ProstheticId);
        var existingProsthetic = await prostheticRepository.GetById(prostheticId, cancellationToken);
        
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match<Task<Result<Request, RequestException>>>(async u =>
            {
                return await existingProsthetic.Match<Task<Result<Request, RequestException>>>(async p => 
                        await CreateEntity(request.Description, userId, prostheticId, cancellationToken),
                    
                    () => Task.FromResult<Result<Request, RequestException>>(
                        new RequestProstheticNotFoundException(prostheticId)));
            },
            () => Task.FromResult<Result<Request, RequestException>>(new RequestUserNotFoundException(userId)));
    }

    private async Task<Result<Request, RequestException>> CreateEntity(
        string requestDescription, 
        UserId userId, 
        ProstheticId prostheticId, 
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Request.New(RequestId.New(), requestDescription, userId, prostheticId);
            return await requestRepository.Add(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new RequestUnknownException(RequestId.Empty(), e);
        }
    }
}
using Domain.Prosthetics;
using Domain.Request;
using Domain.Users;

namespace Application.Requests.Exceptions;

public abstract class RequestException(RequestId requestId, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public RequestId RequestId { get; } = requestId;
}
public class RequestNotFoundException(RequestId requestId)  : RequestException(requestId, $"Request with ID {requestId} not found.");

public class RequestAlreadyExistsException(RequestId requestId) : RequestException(requestId, $"Request with ID {requestId} already exists.");

public class RequestUnknownException(RequestId requestId, Exception innerException) : RequestException(requestId, $"An unknown error occurred with Request ID {requestId}.", innerException);

public class RequestUserNotFoundException(UserId userId) : RequestException(RequestId.Empty(), $"User with ID {userId} not found.");

public class RequestProstheticNotFoundException(ProstheticId prostheticId) : RequestException(RequestId.Empty(), $"Prosthetic with ID {prostheticId} not found.");
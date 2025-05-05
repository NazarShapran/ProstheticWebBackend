using Domain.Functionalities;

namespace Application.Functionalities.Exceptions;

public abstract class FunctionalityException(FunctionalityId id, string message, Exception? innerException = null) 
    : Exception(message, innerException)
{
    public FunctionalityId FunctionalityId { get; } = id;
}

public class FunctionalityNotFoundException(FunctionalityId id) : FunctionalityException(id, $"Functionality under id: {id} not found");

public class FunctionalityAlreadyExistsException(FunctionalityId id) : FunctionalityException(id, $"Functionality already exists: {id}");

public class FunctionalityUnknownException(FunctionalityId id, Exception innerException)
    : FunctionalityException(id, $"Unknown exception for the Functionality under id: {id}", innerException);
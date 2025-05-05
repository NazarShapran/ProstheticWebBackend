using Domain.ProstheticTypes;

namespace Application.ProstheticTypes.Exceptions;

public abstract class ProstheticTypeException(TypeId id, string message, Exception? innerException = null) 
    : Exception(message, innerException)        
{
    public TypeId TypeId { get; } = id;
}

public class ProstheticTypeNotFoundException(TypeId id) : ProstheticTypeException(id, $"Prosthetic Type under id: {id} not found");

public class ProstheticTypeAlreadyExistsException(TypeId id) : ProstheticTypeException(id, $"Prosthetic Type already exists: {id}");

public class ProstheticTypeUnknownException(TypeId id, Exception innerException)
    : ProstheticTypeException(id, $"Unknown exception for the Prosthetic Type under id: {id}", innerException);
using Domain.ProstheticStatuses;

namespace Application.ProstheticStatuses.Exceptions;

public abstract class ProstheticStatusException(ProstheticStatusId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ProstheticStatusId Id { get; } = id;
}

public class ProstheticStatusNotFoundException(ProstheticStatusId id)
    : ProstheticStatusException(id, $"ProstheticStatus with id {id.Value} not found")
{
}

public class ProstheticStatusAlreadyExistsException(ProstheticStatusId id)
    : ProstheticStatusException(id, $"ProstheticStatus with id {id.Value} already exists")
{
}

public class ProstheticStatusUnknownException(ProstheticStatusId id, Exception innerException)
    : ProstheticStatusException(id, "Unknown error occurred", innerException)
{
} 
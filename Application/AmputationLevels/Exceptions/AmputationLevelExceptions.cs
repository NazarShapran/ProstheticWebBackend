using Domain.AmputationLevels;

namespace Application.AmputationLevels.Exceptions;

public abstract class AmputationLevelException(AmputationLevelId id, string message, Exception? innerException = null) 
    : Exception(message, innerException)
{
    public AmputationLevelId AmputationLevelIdId { get; } = id;
}

public class AmputationLevelNotFoundException(AmputationLevelId id) : AmputationLevelException(id, $"AmputationLevel under id: {id} not found");

public class AmputationLevelAlreadyExistsException(AmputationLevelId id) : AmputationLevelException(id, $"AmputationLevel already exists: {id}");

public class AmputationLevelUnknownException(AmputationLevelId id, Exception innerException)
    : AmputationLevelException(id, $"Unknown exception for the AmputationLevel under id: {id}", innerException);
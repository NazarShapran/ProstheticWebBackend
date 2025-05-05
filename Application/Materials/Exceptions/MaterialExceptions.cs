using Domain.Materials;

namespace Application.Materials.Exceptions;

public abstract class MaterialException(MaterialId id, string message, Exception? innerException = null) 
    : Exception(message, innerException)
{
    public MaterialId MaterialIdId { get; } = id;
}

public class MaterialNotFoundException(MaterialId id) : MaterialException(id, $"Material under id: {id} not found");

public class MaterialAlreadyExistsException(MaterialId id) : MaterialException(id, $"Material already exists: {id}");

public class MaterialUnknownException(MaterialId id, Exception innerException)
    : MaterialException(id, $"Unknown exception for the Material under id: {id}", innerException);
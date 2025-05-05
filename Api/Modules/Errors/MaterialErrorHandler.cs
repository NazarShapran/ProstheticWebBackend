using Application.Materials.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class MaterialErrorHandler
{
    public static ObjectResult ToObjectResult(this MaterialException exceptions)
    {
        return new ObjectResult(exceptions.Message)
        {
            StatusCode = exceptions switch
            {
                MaterialNotFoundException => StatusCodes.Status404NotFound,
                MaterialAlreadyExistsException => StatusCodes.Status409Conflict,
                MaterialUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Material error handler does not implemented")
            }
        };
    }
}
using Application.ProstheticTypes.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class ProstheticTypeErrorHandler
{
    public static ObjectResult ToObjectResult(this ProstheticTypeException exceptions)
    {
        return new ObjectResult(exceptions.Message)
        {
            StatusCode = exceptions switch
            {
                ProstheticTypeNotFoundException => StatusCodes.Status404NotFound,
                ProstheticTypeAlreadyExistsException => StatusCodes.Status409Conflict,
                ProstheticTypeUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("ProstheticType error handler does not implemented")
            }
        };
    }
}
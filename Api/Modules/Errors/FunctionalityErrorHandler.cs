using Application.Functionalities.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class FunctionalityErrorHandler
{
    public static ObjectResult ToObjectResult(this FunctionalityException exceptions)
    {
        return new ObjectResult(exceptions.Message)
        {
            StatusCode = exceptions switch
            {
                FunctionalityNotFoundException => StatusCodes.Status404NotFound,
                FunctionalityAlreadyExistsException => StatusCodes.Status409Conflict,
                FunctionalityUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Functionality error handler does not implemented")
            }
        };
    }
}
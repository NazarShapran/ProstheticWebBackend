using Application.ProstheticStatuses.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class ProstheticStatusesErrorHandler
{
    public static ObjectResult ToObjectResult(this ProstheticStatusException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                ProstheticStatusNotFoundException => StatusCodes.Status404NotFound,
                ProstheticStatusAlreadyExistsException => StatusCodes.Status409Conflict,
                ProstheticStatusUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Status error handler does not implemented")
            }
        };
    }
}
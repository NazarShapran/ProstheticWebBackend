using Application.AmputationLevels.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class AmputationLevelErrorHandler
{
    public static ObjectResult ToObjectResult(this AmputationLevelException exceptions)
    {
        return new ObjectResult(exceptions.Message)
        {
            StatusCode = exceptions switch
            {
                AmputationLevelNotFoundException => StatusCodes.Status404NotFound,
                AmputationLevelAlreadyExistsException => StatusCodes.Status409Conflict,
                AmputationLevelUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("AmputationLevel error handler does not implemented")
            }
        };
    }
}
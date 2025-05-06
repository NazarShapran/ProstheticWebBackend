using Application.Reviews.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class ReviewErrorHandler
{
    public static ObjectResult ToObjectResult(this ReviewException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                ReviewNotFoundException or
                    ReviewUserNotFoundException or
                    ReviewProstheticNotFoundException => StatusCodes.Status404NotFound,
                ReviewAlreadyExistsException => StatusCodes.Status409Conflict,
                ReviewUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Reviews error handler does not implemented")
            }
        };
    }
}
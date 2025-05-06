using Application.Requests.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class RequestErrorHandler
{
    public static ObjectResult ToObjectResult(this RequestException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                RequestNotFoundException or
                    RequestUserNotFoundException or
                    RequestProstheticNotFoundException => StatusCodes.Status404NotFound,
                RequestAlreadyExistsException => StatusCodes.Status409Conflict,
                RequestUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Requests error handler does not implemented")
            }
        };
    }
}
﻿using Application.Roles.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class RoleErrorHandler
{
    public static ObjectResult ToObjectResult(this RoleExceptions exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                RoleNotFoundException => StatusCodes.Status404NotFound,
                RoleAlreadyExistsException => StatusCodes.Status409Conflict,
                RoleUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Role error handler does not implemented")
            }
        };
    }
}
﻿using HelperPE.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ProblemDetails problemDetails = new ProblemDetails();

            if (exception is CustomException customException)
                problemDetails = new ProblemDetails
                {
                    Status = customException.Code,
                    Title = customException.Error,
                    Detail = customException.Message
                };
            else if (exception is UnauthorizedAccessException)
                problemDetails = new ProblemDetails
                {
                    Status = 401,
                    Title = "Unauthorized!",
                    Detail = "Refresh or access token is not valid"
                };
            else
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal server error"
                };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
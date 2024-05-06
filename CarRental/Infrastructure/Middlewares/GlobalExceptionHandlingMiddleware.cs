// <copyright file="GlobalExceptionHandlingMiddleware.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.Text.Json;
using CarRental.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Infrastructure.Middlewares;

public class GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> log)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ServiceException ex)
        {
            await HandleServiceException(ex, context);
        }
        catch (Exception ex)
        {
            await HandleException(ex, context);
        }
    }

    private Task HandleServiceException(ServiceException ex, HttpContext context)
    {
        log.LogError(ex, "An expected exception has occured with {TraceId} TraceId.", context.TraceIdentifier);

        var details = new ProblemDetails
        {
            Title = ex.Title,
            Detail = ex.Message,
            Status = ex.StatusCode,
            Extensions =
            {
                ["traceId"] = context.TraceIdentifier,
            },
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex.StatusCode;

        var serializedDetails = JsonSerializer.Serialize(details);

        return context.Response.WriteAsync(serializedDetails);
    }

    private Task HandleException(Exception ex, HttpContext context)
    {
        log.LogError(ex, "An unhandled exception has occured with {TraceId} TraceId.", context.TraceIdentifier);

        var details = new ProblemDetails
        {
            Title = "Unhandled exception",
            Detail = "An unhandled exception has occured.",
            Status = StatusCodes.Status500InternalServerError,
            Extensions =
            {
                ["traceId"] = context.TraceIdentifier,
            },
        };

        var serializedDetails = JsonSerializer.Serialize(details);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return context.Response.WriteAsync(serializedDetails);
    }
}
// <copyright file="AccountHelperMiddleware.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Services.Interfaces;

namespace CarRental.Infrastructure.Middlewares;

public class AccountHelperMiddleware(RequestDelegate next)
{
    public Task InvokeAsync(HttpContext context, IAccountHelper accountHelper)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            accountHelper.EmailAddress = context.User.Claims.FirstOrDefault(c => c.Type == "email")!.Value;
        }

        return next(context);
    }
}
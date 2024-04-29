// <copyright file="WebApplicationBuilderExtensions.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services;
using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using CarRental.Domain.Services;
using CarRental.Domain.Services.Interfaces;
using CarRental.Infrastructure.Services;
using CarRental.Infrastructure.Services.Interfaces;

namespace CarRental.Domain.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddBusinessLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IUserService, UserService>();
    }

    public static void AddPersistenceLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IUserRepository, UserRepository>();
    }

    public static void AddInfrastructureLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasherService, PasswordHasherService<User>>();
    }
}
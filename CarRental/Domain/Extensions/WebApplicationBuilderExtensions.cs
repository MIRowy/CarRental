// <copyright file="WebApplicationBuilderExtensions.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.Security.Cryptography;
using CarRental.Database.Services;
using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using CarRental.Domain.Services;
using CarRental.Domain.Services.Interfaces;
using CarRental.Infrastructure.Extensions;
using CarRental.Infrastructure.Services;
using CarRental.Infrastructure.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Domain.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddBusinessLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICarModelService, CarModelService>();
        builder.Services.AddSingleton<ICarRentService, CarRentService>();
        builder.Services.AddSingleton<ICarReservationService, CarReservationService>();
        builder.Services.AddSingleton<ICarService, CarService>();
        builder.Services.AddSingleton<IUserService, UserService>();
    }

    public static void AddPersistenceLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICarFailureRepository, CarFailureRepository>();
        builder.Services.AddSingleton<ICarModelRepository, CarModelRepository>();
        builder.Services.AddSingleton<ICarRentRepository, CarRentRepository>();
        builder.Services.AddSingleton<ICarRepository, CarRepository>();
        builder.Services.AddSingleton<ICarReservationRepository, CarReservationRepository>();
        builder.Services.AddSingleton<ICarReturnRepository, CarReturnRepository>();
        builder.Services.AddSingleton<IUserRepository, UserRepository>();
    }

    public static void AddInfrastructureLayer(this WebApplicationBuilder builder)
    {
        var rsaKey = RSA.Create();
        var key = new RsaSecurityKey(rsaKey);

        builder.Services.AddSingleton<IJwtService>(new JwtService(key));

        builder.AddCarRentalAuth(key);
        builder.AddMongoDb();

        builder.Services.AddScoped<IAccountHelper, AccountHelper>();
        builder.Services.AddSingleton<IPasswordHasherService, PasswordHasherService<User>>();
    }
}
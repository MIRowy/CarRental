// <copyright file="WebApplicationBuilderExtensions.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace CarRental.Infrastructure.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddMongoDb(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("CarRental");

        ArgumentNullException.ThrowIfNull(database);

        builder.Services.AddSingleton(database);
    }

    public static void AddCarRentalAuth(this WebApplicationBuilder builder, SecurityKey key)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = true;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(ApplicationRoles.User), policy =>
            {
                policy.RequireClaim("role", nameof(ApplicationRoles.User), nameof(ApplicationRoles.Employee));
            });

            options.AddPolicy(nameof(ApplicationRoles.Employee), policy =>
            {
                policy.RequireClaim("role", nameof(ApplicationRoles.Employee));
            });
        });
    }
}
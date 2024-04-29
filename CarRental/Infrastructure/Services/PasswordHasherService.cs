﻿// <copyright file="PasswordHasherService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Infrastructure.Services;

public class PasswordHasherService<TUser> : IPasswordHasherService
    where TUser : class
{
    private readonly PasswordHasher<TUser> passwordHasher = new();

    public string HashPassword(string password)
    {
        return this.passwordHasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var result = this.passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);

        return result == PasswordVerificationResult.Success;
    }
}
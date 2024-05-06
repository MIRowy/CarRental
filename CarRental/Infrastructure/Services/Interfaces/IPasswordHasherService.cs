// <copyright file="IPasswordHasherService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Identity;

namespace CarRental.Infrastructure.Services.Interfaces;

/// <summary>
/// The interface holding contracts for managing passwords.
/// </summary>
public interface IPasswordHasherService
{
    /// <summary>
    /// Hashes password using internal <see cref="PasswordHasher{TUser}"/>.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>Hashed password.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies if provided password is the same as the hashed one using internal <see cref="PasswordHasher{TUser}"/>.
    /// </summary>
    /// <param name="hashedPassword">The hashed password.</param>
    /// <param name="providedPassword">The provided password.</param>
    /// <returns>True if passwords match, false otherwise.</returns>
    bool VerifyPassword(string hashedPassword, string providedPassword);
}
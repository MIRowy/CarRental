// <copyright file="IUserService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Dto;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CarRental.Domain.Services.Interfaces;

/// <summary>
/// The interface holding contracts for managing users.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="dto">The dto from the infrastructure layer.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    public Task Add(CreateUserDto dto);

    /// <summary>
    /// Creates new JWT for given user credentials.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <param name="password">The password.</param>
    /// <returns>JWT if user exists.</returns>
    public Task<JsonWebToken> GetJwt(string emailAddress, string password);
}
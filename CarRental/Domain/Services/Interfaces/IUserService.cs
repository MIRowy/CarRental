// <copyright file="IUserService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Dto;
using CarRental.Domain.Models;
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
    Task Add(CreateUserDto dto);

    /// <summary>
    /// Gets user from the persistence layer by his email address.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <returns>A <see cref="User"/>.</returns>
    Task<User> Get(string emailAddress);

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>Users collection.</returns>
    Task<List<User>> GetAll();

    /// <summary>
    /// Creates new JWT for given user credentials.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <param name="password">The password.</param>
    /// <returns>JWT if user exists.</returns>
    Task<JsonWebToken> GetJwt(string emailAddress, string password);
}
// <copyright file="IUserRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Models;

namespace CarRental.Database.Services.Interfaces;

/// <summary>
/// A repository for handling user persistence operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Adds user to the database.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    public Task Add(User user);

    /// <summary>
    /// Gets user by the email address.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <returns>User if found, null otherwise.</returns>
    public Task<User> Get(string emailAddress);
}
// <copyright file="IUserRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Models;
using MongoDB.Driver;

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
    Task Add(User user);

    /// <summary>
    /// Gets user by the email address.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <returns>User if found, null otherwise.</returns>
    Task<User> Get(string emailAddress);

    /// <summary>
    /// Gets all users from the database.
    /// </summary>
    /// <returns>Collection of all users.</returns>
    Task<List<User>> GetAll();

    /// <summary>
    /// Updates user by his email address.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <param name="updateDefinition">The user.</param>
    /// <returns>Updated user.</returns>
    Task<User> Update(string emailAddress, UpdateDefinition<User> updateDefinition);

    /// <summary>
    /// Removes user from database by email address.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    Task Delete(string emailAddress);
}
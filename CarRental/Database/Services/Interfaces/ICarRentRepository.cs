// <copyright file="ICarRentRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services.Interfaces;

/// <summary>
/// Interface for managing rental operations in the car rental system.
/// </summary>
public interface ICarRentRepository
{
    /// <summary>
    /// Adds a new rental to the repository.
    /// </summary>
    /// <param name="carRent">The rental to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Add(CarRent carRent);

    /// <summary>
    /// Retrieves a rental by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental.</param>
    /// <returns>A task representing the asynchronous operation. The rental corresponding to the provided ID.</returns>
    Task<CarRent> Get(string id);

    /// <summary>
    /// Retrieves a rental by its unique identifier.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="id">The unique identifier of the rental.</param>
    /// <returns>A task representing the asynchronous operation. The rental corresponding to the provided ID.</returns>
    Task<CarRent> Get(string userId, string id);

    /// <summary>
    /// Retrieves all rentals from the repository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. A list of all rentals.</returns>
    Task<List<CarRent>> GetAll();

    /// <summary>
    /// Updates an existing rental with the provided data.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to update.</param>
    /// <param name="updateDefinition">The updated rental data.</param>
    /// <returns>A task representing the asynchronous operation. The updated rental.</returns>
    Task<CarRent> Update(string id, UpdateDefinition<CarRent> updateDefinition);

    /// <summary>
    /// Deletes a rental from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(string id);
}
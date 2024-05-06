// <copyright file="ICarReturnRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services.Interfaces;

/// <summary>
/// Interface for managing car return operations in the car rental system.
/// </summary>
public interface ICarReturnRepository
{
    /// <summary>
    /// Adds a new car return record to the repository.
    /// </summary>
    /// <param name="carReturn">The car return record to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Add(CarReturn carReturn);

    /// <summary>
    /// Retrieves a car return record by its unique identifier.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="id">The unique identifier of the car return record.</param>
    /// <returns>A task representing the asynchronous operation. The car return record corresponding to the provided ID.</returns>
    Task<CarReturn> Get(string userId, string id);

    /// <summary>
    /// Retrieves all car return records from the repository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. A list of all car return records.</returns>
    Task<List<CarReturn>> GetAll();

    /// <summary>
    /// Updates an existing car return record with the provided update definition.
    /// </summary>
    /// <param name="id">The unique identifier of the car return record to update.</param>
    /// <param name="updateDefinition">The update definition to apply to the car return record.</param>
    /// <returns>A task representing the asynchronous operation. The updated car return record.</returns>
    Task<CarReturn> Update(string id, UpdateDefinition<CarReturn> updateDefinition);

    /// <summary>
    /// Deletes a car return record from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car return record to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(string id);
}
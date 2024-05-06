// <copyright file="ICarFailureRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services.Interfaces;

/// <summary>
/// Interface for managing car failure operations in the car rental system.
/// </summary>
public interface ICarFailureRepository
{
    /// <summary>
    /// Adds a new car failure to the repository.
    /// </summary>
    /// <param name="carFailure">The car failure to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Add(CarFailure carFailure);

    /// <summary>
    /// Retrieves a car failure by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car failure.</param>
    /// <returns>A task representing the asynchronous operation. The car failure corresponding to the provided ID.</returns>
    Task<CarFailure> Get(string id);

    /// <summary>
    /// Retrieves all car failures from the repository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. A list of all car failures.</returns>
    Task<List<CarFailure>> GetAll();

    /// <summary>
    /// Updates an existing car failure with the provided update definition.
    /// </summary>
    /// <param name="id">The unique identifier of the car failure to update.</param>
    /// <param name="updateDefinition">The update definition to apply to the car failure.</param>
    /// <returns>A task representing the asynchronous operation. The updated car failure.</returns>
    Task<CarFailure> Update(string id, UpdateDefinition<CarFailure> updateDefinition);

    /// <summary>
    /// Deletes a car failure from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car failure to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(string id);
}
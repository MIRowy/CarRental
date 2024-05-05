// <copyright file="ICarRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services.Interfaces;

/// <summary>
/// Interface for managing car operations in the car rental system.
/// </summary>
public interface ICarRepository
{
    /// <summary>
    /// Adds a new car to the repository.
    /// </summary>
    /// <param name="car">The car to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Add(Car car);

    /// <summary>
    /// Retrieves a car by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car.</param>
    /// <returns>A task representing the asynchronous operation. The car corresponding to the provided ID.</returns>
    Task<Car> Get(string id);

    /// <summary>
    /// Retrieves all cars from the repository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. A list of all cars.</returns>
    Task<List<Car>> GetAll();

    /// <summary>
    /// Updates an existing car with the provided data.
    /// </summary>
    /// <param name="id">The unique identifier of the car to update.</param>
    /// <param name="updateDefinition">The updated car data.</param>
    /// <returns>A task representing the asynchronous operation. The updated car.</returns>
    Task<Car> Update(string id, UpdateDefinition<Car> updateDefinition);

    /// <summary>
    /// Deletes a car from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(string id);
}
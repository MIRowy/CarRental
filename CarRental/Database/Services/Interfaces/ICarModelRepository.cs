// <copyright file="ICarModelRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services.Interfaces;

/// <summary>
/// Interface for managing car model operations in the car rental system.
/// </summary>
public interface ICarModelRepository
{
    /// <summary>
    /// Adds a new car model to the repository.
    /// </summary>
    /// <param name="model">The car model to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Add(CarModel model);

    /// <summary>
    /// Retrieves a car model by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car model.</param>
    /// <returns>A task representing the asynchronous operation. The car model corresponding to the provided ID.</returns>
    Task<CarModel> Get(string id);

    /// <summary>
    /// Retrieves all car models from the repository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. A list of all car models.</returns>
    Task<List<CarModel>> GetAll();

    /// <summary>
    /// Updates an existing car model with the provided data.
    /// </summary>
    /// <param name="id">The unique identifier of the car model to update.</param>
    /// <param name="updateDefinition">The updated car model data.</param>
    /// <returns>A task representing the asynchronous operation. The updated car model.</returns>
    Task<CarModel> Update(string id, UpdateDefinition<CarModel> updateDefinition);

    /// <summary>
    /// Deletes a car model from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car model to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(string id);
}
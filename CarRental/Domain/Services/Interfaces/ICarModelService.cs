// <copyright file="ICarModelService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Dto;
using CarRental.Domain.Models;

namespace CarRental.Domain.Services.Interfaces;

/// <summary>
/// Interface for managing car model service operations in the car rental system.
/// </summary>
public interface ICarModelService
{
    /// <summary>
    /// Adds a new car model to the service.
    /// </summary>
    /// <param name="dto">Data transfer object containing information for adding a new car model.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<CarModel> Add(AddCarModelDto dto);

    /// <summary>
    /// Retrieves a car model by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car model.</param>
    /// <returns>A task representing the asynchronous operation. The car model corresponding to the provided ID.</returns>
    Task<CarModel> Get(string id);

    /// <summary>
    /// Retrieves all car models from the service.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. A list of all car models.</returns>
    Task<List<CarModel>> GetAll();

    /// <summary>
    /// Updates an existing car model with the provided data.
    /// </summary>
    /// <param name="dto">Data transfer object containing information for updating an existing car model.</param>
    /// <returns>A task representing the asynchronous operation. The updated car model.</returns>
    Task<CarModel> Update(UpdateCarModelDto dto);

    /// <summary>
    /// Deletes a car model from the service by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car model to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(string id);
}
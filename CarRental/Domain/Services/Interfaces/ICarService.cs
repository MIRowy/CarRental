// <copyright file="ICarService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Dto;
using CarRental.Domain.Models;

namespace CarRental.Domain.Services.Interfaces;

/// <summary>
/// The interface holding contracts for managing car domain.
/// </summary>
public interface ICarService
{
    /// <summary>
    /// Creates the car model out of the infrastructure data.
    /// </summary>
    /// <param name="dto">Object holding information about the new car.</param>
    /// <returns>A <see cref="Car"/>.</returns>
    Task<Car> Add(AddCarDto dto);

    /// <summary>
    /// Retrieves a car from the persistence layer.
    /// </summary>
    /// <param name="id">The car ID.</param>
    /// <returns>A <see cref="Car"/>.</returns>
    Task<Car> Get(string id);

    /// <summary>
    /// Retrieves all the cars from the persistence layer.
    /// </summary>
    /// <returns>A collection of <see cref="Car"/>.</returns>
    Task<List<Car>> GetAll();

    /// <summary>
    /// Updates existing car.
    /// </summary>
    /// <param name="dto">Object holding information about the car.</param>
    /// <returns>An updated <see cref="Car"/>.</returns>
    Task<Car> Update(UpdateCarDto dto);

    /// <summary>
    /// Removes a car from the persistence layer.
    /// </summary>
    /// <param name="id">The car ID.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(string id);
}
// <copyright file="ICarService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Models;

namespace CarRental.Services.Interfaces;

/// <summary>
/// The interface holding contracts for managing car domain.
/// </summary>
public interface ICarService
{
    /// <summary>
    /// Creates the car model out of the infrastructure data. 
    /// </summary>
    /// <param name="car"></param>
    /// <returns>A <see cref="Car"/>.</returns>
    Task<Car> Create(Car car);

    /// <summary>
    /// Retrieves a car from the persistence layer.
    /// </summary>
    /// <param name="id">The car ID.</param>
    /// <returns>A <see cref="Car"/>.</returns>
    Task<Car> Get(Guid id);

    /// <summary>
    /// Retrieves all of the cars from the persistence layer.
    /// </summary>
    /// <returns>A collection of <see cref="Car"/>.</returns>
    Task<IEnumerable<Car>> GetAll();

    /// <summary>
    /// Updates existing car.
    /// </summary>
    /// <param name="car"></param>
    /// <returns>An updated <see cref="Car"/>.</returns>
    Task<Car> Update(Car car);

    /// <summary>
    /// Removes a car from the persistence layer.
    /// </summary>
    /// <param name="id">The car ID.</param>
    /// <returns>A deleted <see cref="Car"/>.</returns>
    Task<Car> Delete(Guid id);
}
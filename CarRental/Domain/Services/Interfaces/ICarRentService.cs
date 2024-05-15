// <copyright file="ICarRentService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Dto;
using CarRental.Domain.Models;

namespace CarRental.Domain.Services.Interfaces;

/// <summary>
/// The interface holding contracts for managing the renting system.
/// </summary>
public interface ICarRentService
{
    /// <summary>
    /// Retrieves a car rent transaction by its unique identifier.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="id">The unique identifier of the car rent transaction.</param>
    /// <returns>A task representing the asynchronous operation. The car rent transaction corresponding to the provided ID.</returns>
    Task<CarRent> Get(string userId, string id);

    /// <summary>
    /// Retrieves all car rent transactions.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. A list of all car rent transactions.</returns>
    Task<List<CarRent>> GetAll();

    /// <summary>
    /// Adds a new car rent transaction.
    /// </summary>
    /// <param name="dto">The data transfer object containing information for adding a new car rent transaction.</param>
    /// <returns>A task representing the asynchronous operation. The added car rent transaction.</returns>
    Task<CarRent> Add(AddCarRentDto dto);

    /// <summary>
    /// Creates a new car failure record.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="dto">The data transfer object containing information for creating a new car failure record.</param>
    /// <returns>A task representing the asynchronous operation. The created car failure record.</returns>
    Task<CarFailure> CreateFailure(string userId, CreateFailureDto dto);

    /// <summary>
    /// Completes a car failure record.
    /// </summary>
    /// <param name="dto">The data transfer object containing information for creating a new car failure record.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    Task<CarFailure> CompleteCarFailure(CompleteCarFailureDto dto);

    /// <summary>
    /// Completes a car rent transaction.
    /// </summary>
    /// <param name="dto">The data transfer object containing information for completing a car rent transaction.</param>
    /// <returns>A task representing the asynchronous operation. The completed car rent transaction.</returns>
    Task<CarReturn> CompleteRent(CompleteCarRentDto dto);
}

// <copyright file="ICarReservationService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Dto;
using CarRental.Domain.Models;

namespace CarRental.Domain.Services.Interfaces;

/// <summary>
/// The interface holding contracts for managing the reservation system.
/// </summary>
public interface ICarReservationService
{
    /// <summary>
    /// Adds a new car reservation.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="dto">The data transfer object containing information for adding a new car reservation.</param>
    /// <returns>A task representing the asynchronous operation. The added car reservation.</returns>
    Task<CarReservation> Add(string userId, AddCarReservationDto dto);

    /// <summary>
    /// Retrieves a car reservation by its unique identifier.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="id">The unique identifier of the car reservation.</param>
    /// <returns>A task representing the asynchronous operation. The car reservation corresponding to the provided ID.</returns>
    Task<CarReservation> Get(string userId, string id);

    /// <summary>
    /// Retrieves all car reservations.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. A list of all car reservations.</returns>
    Task<List<CarReservation>> GetAll();

    /// <summary>
    /// Retrieves car reservations for a specific car model within the specified date range.
    /// </summary>
    /// <param name="carModelId">The unique identifier of the car model.</param>
    /// <param name="startDate">The start date of the reservation period.</param>
    /// <param name="endDate">The end date of the reservation period.</param>
    /// <returns>A task representing the asynchronous operation. A list of car reservations for the specified car model within the specified date range.</returns>
    Task<List<GetCarReservationsDto>> GetForCarModelIdBetweenDates(string carModelId, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Updates an existing car reservation.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="dto">The data transfer object containing information for updating an existing car reservation.</param>
    /// <returns>A task representing the asynchronous operation. The updated car reservation.</returns>
    Task<CarReservation> Update(string userId, UpdateCarReservationDto dto);

    /// <summary>
    /// Deletes a car reservation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the car reservation to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(string id);
}
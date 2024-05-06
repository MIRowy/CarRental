// <copyright file="ICarReservationRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services.Interfaces;

/// <summary>
/// Interface for managing reservation operations in the car rental system.
/// </summary>
public interface ICarReservationRepository
{
    /// <summary>
    /// Adds a new reservation to the repository.
    /// </summary>
    /// <param name="carReservation">The reservation to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Add(CarReservation carReservation);

    /// <summary>
    /// Retrieves a reservation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the reservation.</param>
    /// <returns>A task representing the asynchronous operation. The reservation corresponding to the provided ID.</returns>
    Task<CarReservation> Get(string id);

    /// <summary>
    /// Retrieves a reservation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the reservation.</param>
    /// <param name="userId">The user ID.</param>
    /// <returns>A task representing the asynchronous operation. The reservation corresponding to the provided ID.</returns>
    Task<CarReservation> Get(string id, string userId);

    /// <summary>
    /// Retrieves all reservations from the repository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. A list of all reservations.</returns>
    Task<List<CarReservation>> GetAll();

    /// <summary>
    /// Retrieves car reservations for a specific car model within the specified date range.
    /// </summary>
    /// <param name="carModelId">The unique identifier of the car model.</param>
    /// <param name="startDate">The start date of the reservation period.</param>
    /// <param name="endDate">The end date of the reservation period.</param>
    /// <returns>A task representing the asynchronous operation. A list of car reservations for the specified car model within the specified date range.</returns>
    Task<List<CarReservation>> GetForCarModelIdBetweenDates(string carModelId, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Updates an existing reservation with the provided data.
    /// </summary>
    /// <param name="id">The unique identifier of the reservation to update.</param>
    /// <param name="updateDefinition">The updated reservation data.</param>
    /// <returns>A task representing the asynchronous operation. The updated reservation.</returns>
    Task<CarReservation> Update(string id, UpdateDefinition<CarReservation> updateDefinition);

    /// <summary>
    /// Deletes a reservation from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the reservation to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Delete(string id);

    /// <summary>
    /// Checks if a car is available for reservation within the specified time frame.
    /// </summary>
    /// <param name="carId">The unique identifier of the car model.</param>
    /// <param name="startDate">The start date of the reservation period.</param>
    /// <param name="endDate">The end date of the reservation period.</param>
    /// <returns>A task representing the asynchronous operation. True if the car is available for reservation, false otherwise.</returns>
    Task<bool> IsCarAvailableForReservation(string carId, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Checks whether a reservation with the provided ID has less than 24H before collection.
    /// </summary>
    /// <param name="carReservationId">The car reservation ID.</param>
    /// <returns>True if less than 24H, false otherwise.</returns>
    Task<bool> Is24HBeforeCollection(string carReservationId);
}
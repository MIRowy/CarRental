// <copyright file="CarReservationNotFoundException.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Exceptions;

namespace CarRental.Domain.Exceptions;

public class CarReservationNotFoundException() : ServiceException(
    "Bad Request",
    "Car reservation with the provided ID was not found.",
    StatusCodes.Status400BadRequest);
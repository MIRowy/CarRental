// <copyright file="CannotDeleteReservation24HBeforeCollectionException.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Exceptions;

namespace CarRental.Domain.Exceptions;

public class CannotDeleteReservation24HBeforeCollectionException() : ServiceException(
    "Bad Request",
    "Cannot delete reservation if there are less than 24 hours till the car collection.",
    StatusCodes.Status400BadRequest);
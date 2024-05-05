// <copyright file="CarNotAvailableException.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Exceptions;

namespace CarRental.Domain.Exceptions;

public class CarNotAvailableException() : ServiceException(
    "Bad Request",
    "A car with the given ID is not available for rent in wanted time scope.",
    StatusCodes.Status400BadRequest);
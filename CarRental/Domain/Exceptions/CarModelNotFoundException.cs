// <copyright file="CarModelNotFoundException.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Exceptions;

namespace CarRental.Domain.Exceptions;

public class CarModelNotFoundException() : ServiceException(
    "Bad Request",
    "Could not find a car model with the provided ID.",
    StatusCodes.Status400BadRequest);
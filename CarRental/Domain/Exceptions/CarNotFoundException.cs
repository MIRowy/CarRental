// <copyright file="CarNotFoundException.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Exceptions;

namespace CarRental.Domain.Exceptions;

public class CarNotFoundException() : ServiceException(
    "Bad Request",
    "Could not find a car with the provided ID.",
    StatusCodes.Status400BadRequest);
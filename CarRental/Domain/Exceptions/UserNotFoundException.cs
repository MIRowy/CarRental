// <copyright file="UserNotFoundException.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Exceptions;

namespace CarRental.Domain.Exceptions;

public class UserNotFoundException() : ServiceException(
    "Bad Request",
    "User with given parameters was not found.",
    StatusCodes.Status400BadRequest);
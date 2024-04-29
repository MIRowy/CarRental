// <copyright file="InvalidEmailAddressOrPasswordException.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Exceptions;

namespace CarRental.Domain.Exceptions;

public class InvalidEmailAddressOrPasswordException()
    : ServiceException(
        "Bad Request",
        "Invalid email address or password was provided.",
        StatusCodes.Status400BadRequest);
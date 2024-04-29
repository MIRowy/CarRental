// <copyright file="ServiceExceptionBase.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

namespace CarRental.Infrastructure.Exceptions;

public class ServiceException(string title, string message, int statusCode) : Exception(message)
{
    public string Title { get; } = title;

    public int StatusCode { get; } = statusCode;
}
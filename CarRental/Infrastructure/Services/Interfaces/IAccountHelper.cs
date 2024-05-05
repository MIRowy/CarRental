// <copyright file="IAccountHelper.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

namespace CarRental.Infrastructure.Services.Interfaces;

/// <summary>
/// Interface for providing account-related helper functionalities.
/// </summary>
public interface IAccountHelper
{
    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    string EmailAddress { get; set; }
}
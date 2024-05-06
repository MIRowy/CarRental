// <copyright file="AccountHelper.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Services.Interfaces;

namespace CarRental.Infrastructure.Services;

public class AccountHelper : IAccountHelper
{
    public string EmailAddress { get; set; } = string.Empty;
}
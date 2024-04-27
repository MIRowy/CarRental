// <copyright file="Reservation.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

namespace CarRental.Models;

internal record Reservation(Car Car, DateTime Start, DateTime End, bool IsDepositPaid)
{
    public Guid Id { get; } = Guid.NewGuid();

    // TODO: Implement this
    public int GetTotalPrice()
    {
        return 0;
    }

    // TODO: Implement this
    public int GetLengthInDays()
    {
        return 0;
    }

    // TODO: Implement this
    public int GetDeposit()
    {
        return 0;
    }
}
// <copyright file="Reservation.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

internal record Reservation(Car Car, DateTime Start, DateTime End, bool IsDepositPaid)
{
    [BsonId]
    public string Id { get; } = Guid.NewGuid().ToString();

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
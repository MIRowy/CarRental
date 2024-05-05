// <copyright file="CarReservation.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public record CarReservation(string UserId, Car Car, DateTime Start, DateTime End, bool IsDepositPaid)
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

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
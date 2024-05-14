// <copyright file="CarReturn.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public record CarReturn(CarRent CarRent, DateTime Date, bool IsCleaningNeeded, bool IsFuelingNeeded)
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();

    // TODO: Implement the below.
    public int GetTotalPrice()
    {
        return 0;
    }

    // TODO: Implement the below.
    private int GetPriceForOvertime()
    {
        return 0;
    }
}
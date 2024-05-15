// <copyright file="CarRent.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public record CarRent(CarReservation CarReservation)
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public CarRentStatuses Status { get; init; } = CarRentStatuses.Ongoing;
}
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
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public RentStatuses Status { get; } = RentStatuses.Ongoing;
}
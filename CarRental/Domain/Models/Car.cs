// <copyright file="Car.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public record Car(CarModel Model, string Description, int Odometer, int PricePerDay, IEnumerable<byte[]>? Images = null)
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public IEnumerable<byte[]> Images { get; } = Images ?? Enumerable.Empty<byte[]>();
}
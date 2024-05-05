// <copyright file="Car.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public record Car(CarModel Model, string Description, int Odometer, IEnumerable<byte[]>? Images = null)
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public IEnumerable<byte[]> Images { get; } = Images ?? Enumerable.Empty<byte[]>();
}
// <copyright file="CarFailure.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public record CarFailure(
    CarRent CarRent,
    string Description,
    [property: BsonIgnoreIfNull]
    bool? IsFastService = null,
    [property: BsonIgnoreIfNull]
    bool? IsAccepted = null)
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
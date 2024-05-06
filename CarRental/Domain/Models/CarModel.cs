// <copyright file="CarModel.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public record CarModel(string Brand, string Variant, int Engine, int Power, string Colour, GearboxTypes Gearbox)
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
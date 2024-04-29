// <copyright file="Car.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

namespace CarRental.Domain.Models;

public record Car(CarModel Model, string Description, int Odometer)
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public IEnumerable<byte[]> Images { get; } = Enumerable.Empty<byte[]>();
}
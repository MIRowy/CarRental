// <copyright file="Car.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

namespace CarRental.Models;

public record Car(CarModel Model, string Description, int Odometer)
{
    public Guid Id { get; } = Guid.NewGuid();

    public IEnumerable<byte[]> Images { get; } = Enumerable.Empty<byte[]>();
}
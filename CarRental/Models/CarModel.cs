// <copyright file="CarModel.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Enums;

namespace CarRental.Models;

public record CarModel(string Brand, string Variant, int Engine, int Power, string Colour, GearboxTypes Gearbox)
{
    public Guid Id { get; } = Guid.NewGuid();
}
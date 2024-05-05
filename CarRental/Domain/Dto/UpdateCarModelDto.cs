// <copyright file="UpdateCarModelDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Dto;

public record UpdateCarModelDto(
    [Required] string Id,
    string? Brand,
    string? Variant,
    int? Engine,
    int? Power,
    string? Colour,
    GearboxTypes? Gearbox);
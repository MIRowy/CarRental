// <copyright file="AddCarModelDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Dto;

public record AddCarModelDto(
    [Required]
    string Brand,
    [Required]
    string Variant,
    [Required]
    int Engine,
    [Required]
    int Power,
    [Required]
    string Colour,
    [Required]
    GearboxTypes Gearbox);
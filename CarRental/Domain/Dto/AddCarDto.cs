// <copyright file="AddCarDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record AddCarDto(
    [Required]
    string CarModelId,
    [Required]
    string Description,
    [Required]
    int OdoMeter,
    [Required]
    int PricePerDay,
    IEnumerable<byte[]>? Images = null);
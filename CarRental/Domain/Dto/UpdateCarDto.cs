// <copyright file="UpdateCarDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record UpdateCarDto(
    [Required]
    string Id,
    string? CarModelId = null,
    string? Description = null,
    int? Odometer = null,
    int? PricePerDay = null,
    IEnumerable<byte[]>? Images = null);
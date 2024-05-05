// <copyright file="UpdateCarDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record UpdateCarDto(
    [Required]
    string Id,
    [Required]
    string CarModelId,
    [Required]
    string Description,
    [Required]
    int Odometer,
    IEnumerable<byte[]>? Images = null);
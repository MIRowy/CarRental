// <copyright file="CreateFailureDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record CreateFailureDto(
    [Required]
    string CarRentId,
    [Required]
    string Description);
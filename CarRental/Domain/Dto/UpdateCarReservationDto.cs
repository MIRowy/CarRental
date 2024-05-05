// <copyright file="UpdateCarReservationDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record UpdateCarReservationDto(
    [Required]
    string CarReservationId,
    [Required]
    string CarId,
    [Required]
    DateTime StartDate,
    [Required]
    DateTime EndDate);
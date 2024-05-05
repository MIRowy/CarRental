// <copyright file="AddCarReservationDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record AddCarReservationDto(
    [Required]
    string CarId,
    [Required]
    DateTime StartDate,
    [Required]
    DateTime EndDate,
    [Required]
    bool IsDepositPaid);
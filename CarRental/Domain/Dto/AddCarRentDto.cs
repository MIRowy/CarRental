// <copyright file="AddCarRentDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record AddCarRentDto(
    [Required]
    string CarReservationId);
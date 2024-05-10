// <copyright file="CompleteCarRentDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record CompleteCarRentDto(
    [Required]
    string CarRentId,
    [Required]
    DateTime Date,
    [Required]
    bool IsCleaningNeeded,
    int LackingGas = 0);
// <copyright file="GetJwtDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record GetJwtDto(
    [EmailAddress]
    [Required]
    string EmailAddress,
    [Required]
    string Password);
// <copyright file="CreateUserDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CarRental.Domain.Attributes;

namespace CarRental.Domain.Dto;

public record CreateUserDto(
    [Required]
    [EmailAddress]
    string EmailAddress,
    [Required]
    [PasswordPropertyText]
    string Password,
    [Required]
    string Name,
    [Required]
    string Surname,
    [Required]
    [Phone]
    string PhoneNumber,
    [Required]
    [Pesel]
    string Pesel);
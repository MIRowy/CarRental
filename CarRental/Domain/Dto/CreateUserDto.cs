// <copyright file="CreateUserDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CarRental.Domain.Attributes;

namespace CarRental.Domain.Dto;

public class CreateUserDto
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }

    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [Pesel]
    public string Pesel { get; set; }
}
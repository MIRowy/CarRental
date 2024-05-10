// <copyright file="CompleteCarFailureDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace CarRental.Domain.Dto;

public record CompleteCarFailureDto(
    [Required]
    string CarFailureId,
    [Required]
    string Description,
    [Required]
    bool IsFastService,
    [Required]
    bool IsAccepted);
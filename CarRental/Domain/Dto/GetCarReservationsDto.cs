// <copyright file="GetCarReservationsDto.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

namespace CarRental.Domain.Dto;

public record GetCarReservationsDto(DateTime StartDate, DateTime EndDate);
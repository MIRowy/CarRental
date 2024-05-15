// <copyright file="CarReservationController.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CarRental.Domain.Dto;
using CarRental.Domain.Services.Interfaces;
using CarRental.Infrastructure.Enums;
using CarRental.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

[ApiController]
[Route("/api/v1/carReservation")]
public class CarReservationController(
    ICarReservationService carReservationService,
    IAccountHelper accountHelper) : ControllerBase
{
    [HttpPost]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> AddCarReservation([Required, FromBody] AddCarReservationDto dto)
    {
        var addedCarReservation = await carReservationService.Add(accountHelper.EmailAddress, dto);

        return Ok(addedCarReservation);
    }

    [HttpGet("{id}")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetCarReservation([Required, FromRoute] string id)
    {
        var carReservation = await carReservationService.Get(accountHelper.EmailAddress, id);

        return Ok(carReservation);
    }

    [HttpGet("all")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetAllCarReservations()
    {
        var carReservations = await carReservationService.GetAll();

        return Ok(carReservations);
    }

    [HttpGet("model/{id}")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetCarReservationsForModelBetweenDates(
        [Required, FromRoute] string id,
        [Required, FromQuery] DateTime start,
        [Required, FromQuery] DateTime end)
    {
        var dtos = await carReservationService.GetForCarModelIdBetweenDates(id, start, end);

        return Ok(dtos);
    }

    [HttpPatch]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> UpdateCarReservation([Required, FromBody] UpdateCarReservationDto dto)
    {
        var updatedCarReservation = await carReservationService.Update(accountHelper.EmailAddress, dto);

        return Ok(updatedCarReservation);
    }

    [HttpDelete("{id}")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> DeleteCarReservation(string id)
    {
        await carReservationService.Delete(id);

        return NoContent();
    }
}
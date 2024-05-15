// <copyright file="CarController.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CarRental.Domain.Dto;
using CarRental.Domain.Services.Interfaces;
using CarRental.Infrastructure.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

[ApiController]
[Route("/api/v1/car")]
public class CarController(ICarService carService) : ControllerBase
{
    [HttpPost]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> AddCar([Required, FromBody] AddCarDto dto)
    {
        var addedCar = await carService.Add(dto);

        return Ok(addedCar);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCar([Required, FromRoute] string id)
    {
        var car = await carService.Get(id);

        return Ok(car);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCars()
    {
        var cars = await carService.GetAll();

        return Ok(cars);
    }

    [HttpPatch]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> UpdateCar([Required, FromBody] UpdateCarDto dto)
    {
        var updatedCar = await carService.Update(dto);

        return Ok(updatedCar);
    }

    [HttpDelete("{id}")]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> DeleteCar(string id)
    {
        await carService.Delete(id);

        return NoContent();
    }
}
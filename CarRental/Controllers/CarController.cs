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

        return this.Ok(addedCar);
    }

    [HttpGet("{id}")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetCar([Required, FromRoute] string id)
    {
        var car = await carService.Get(id);

        return this.Ok(car);
    }

    [HttpGet("all")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetAllCars()
    {
        var cars = await carService.GetAll();

        return this.Ok(cars);
    }

    [HttpPatch]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> UpdateCar([Required, FromBody] UpdateCarDto dto)
    {
        var updatedCar = await carService.Update(dto);

        return this.Ok(updatedCar);
    }

    [HttpDelete("{id}")]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> DeleteCar(string id)
    {
        await carService.Delete(id);

        return this.NoContent();
    }
}
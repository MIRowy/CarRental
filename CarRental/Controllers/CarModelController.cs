// <copyright file="CarModelController.cs" company="Car Rental Inc">
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
[Route("/api/v1/carModel")]
public class CarModelController(ICarModelService carModelService) : ControllerBase
{
    [HttpPost]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> AddCarModel([Required, FromBody] AddCarModelDto dto)
    {
        var addedCarModel = await carModelService.Add(dto);

        return this.Ok(addedCarModel);
    }

    [HttpGet("{id}")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetCarModel([Required, FromRoute] string id)
    {
        var carModel = await carModelService.Get(id);

        return this.Ok(carModel);
    }

    [HttpGet("all")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetAllCarModels()
    {
        var carModels = await carModelService.GetAll();

        return this.Ok(carModels);
    }

    [HttpPatch]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> UpdateCarModel([Required, FromBody] UpdateCarModelDto dto)
    {
        var updatedCarModel = await carModelService.Update(dto);

        return this.Ok(updatedCarModel);
    }

    [HttpDelete("{id}")]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> DeleteCarModel(string id)
    {
        await carModelService.Delete(id);

        return this.NoContent();
    }
}
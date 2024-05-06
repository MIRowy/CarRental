// <copyright file="CarRentController.cs" company="Car Rental Inc">
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
[Route("/api/v1/carRent")]
public class CarRentController(
    ICarRentService carRentService,
    IAccountHelper accountHelper) : ControllerBase
{
    [HttpPost]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> AddCarRent([Required, FromBody] AddCarRentDto dto)
    {
        var addedCar = await carRentService.Add(dto);

        return Ok(addedCar);
    }

    [HttpGet("{id}")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetCarRent([Required, FromRoute] string id)
    {
        var car = await carRentService.Get(accountHelper.EmailAddress, id);

        return Ok(car);
    }

    [HttpGet("all")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetAllCarRents()
    {
        var cars = await carRentService.GetAll();

        return Ok(cars);
    }

    [HttpPost("failure")]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> CreateCarFailure(CreateFailureDto dto)
    {
        var carFailure = await carRentService.CreateFailure(accountHelper.EmailAddress, dto);

        return Ok(carFailure);
    }

    [HttpPost("complete")]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> CompleteCarRent(CompleteCarRentDto dto)
    {
        var carReturn = await carRentService.CompleteRent(dto);

        return Ok(carReturn);
    }
}
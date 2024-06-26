﻿// <copyright file="ServiceHealthController.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Enums;
using CarRental.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

[ApiController]
[Route("/api/v1/serviceHealth")]
public class ServiceHealthController(IAccountHelper accountHelper) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("I'm alive!");
    }

    [HttpGet("/employee")]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public IActionResult TestAuthorizeEmployee()
    {
        return Ok($"Yes, you are a brave employee ({accountHelper.EmailAddress}). Good day to you!");
    }

    [HttpGet("/signedUser")]
    [Authorize(nameof(ApplicationRoles.User))]
    public IActionResult TestAuthorizeSignedUser()
    {
        return Ok("Yes, you are a logged user. Congrats!");
    }
}
// <copyright file="ServiceHealthController.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

[ApiController]
[Route("/api/v1/serviceHealth")]
public class ServiceHealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return this.Ok("I'm alive!");
    }
}
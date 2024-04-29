// <copyright file="ServiceHealthController.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Enums;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("/employee")]
    [Authorize(Roles = nameof(ApplicationRoles.Employee))]
    public IActionResult TestAuthorizeEmployee()
    {
        return this.Ok("Yes, you are a brave employee. Good day to you!");
    }

    [HttpGet("/signedUser")]
    [Authorize(Roles = nameof(ApplicationRoles.User))]
    public IActionResult TestAuthorizeSignedUser()
    {
        return this.Ok("Yes, you are a logged user. Congrats!");
    }
}
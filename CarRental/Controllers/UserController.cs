// <copyright file="UserController.cs" company="Car Rental Inc">
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
[Route("/api/v1/user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([Required, FromBody] AddUserDto dto)
    {
        await userService.Add(dto);

        return Created();
    }

    [HttpGet]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetUser()
    {
        var emailAddress = HttpContext.User.FindFirst(a => a.Type == "email");

        // Authorization framework ensures that email is always provided.
        var user = await userService.Get(emailAddress!.Value);

        return Ok(user);
    }

    [HttpGet("{emailAddress}")]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> GetUser([FromRoute] string emailAddress)
    {
        var user = await userService.Get(emailAddress);

        return Ok(user);
    }

    [HttpGet("all")]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAll();

        return Ok(users);
    }

    [HttpPost]
    [Route("jwt")]
    public async Task<IActionResult> GetJwt([Required, FromBody] GetJwtDto dto)
    {
        var token = await userService.GetJwt(dto);

        return Ok(token.EncodedToken);
    }
}
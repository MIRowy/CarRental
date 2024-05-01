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
    public async Task<IActionResult> CreateUser([Required, FromBody] CreateUserDto dto)
    {
        await userService.Add(dto);

        return this.Created();
    }

    [HttpGet]
    [Authorize(nameof(ApplicationRoles.User))]
    public async Task<IActionResult> GetUser()
    {
        var emailAddress = this.HttpContext.User.FindFirst(a => a.Type == "email");

        // Authorization framework ensures that email is always provided.
        var user = await userService.Get(emailAddress!.Value);

        return this.Ok(user);
    }

    [HttpGet("all")]
    [Authorize(nameof(ApplicationRoles.Employee))]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAll();

        return this.Ok(users);
    }

    [HttpPost]
    [Route("jwt")]
    public async Task<IActionResult> GetJwt(string emailAddress, string password)
    {
        var token = await userService.GetJwt(emailAddress, password);

        return this.Ok(token.EncodedToken);
    }
}
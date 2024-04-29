// <copyright file="UserController.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using CarRental.Domain.Dto;
using CarRental.Domain.Services.Interfaces;
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

    [HttpPost]
    [Route("jwt")]
    public async Task<IActionResult> GetJwt(string emailAddress, string password)
    {
        var token = await userService.GetJwt(emailAddress, password);

        return this.Ok(token.EncodedToken);
    }
}
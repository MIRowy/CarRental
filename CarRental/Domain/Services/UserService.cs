// <copyright file="UserService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.Security.Claims;
using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Dto;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.Domain.Services.Interfaces;
using CarRental.Infrastructure.Services.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CarRental.Domain.Services;

public class UserService(
    IJwtService jwtService,
    IPasswordHasherService passwordHasherService,
    IUserRepository userRepository) : IUserService
{
    public Task Add(CreateUserDto dto)
    {
        var hashedPassword = passwordHasherService.HashPassword(dto.Password);
        var user = new User(
            dto.EmailAddress,
            hashedPassword,
            dto.Name,
            dto.Surname,
            dto.PhoneNumber,
            dto.Pesel);

        return userRepository.Add(user);
    }

    public async Task<User> Get(string emailAddress)
    {
        var user = await userRepository.Get(emailAddress);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        return user;
    }

    public Task<List<User>> GetAll() => userRepository.GetAll();

    public async Task<JsonWebToken> GetJwt(string emailAddress, string password)
    {
        var user = await userRepository.Get(emailAddress);

        if (user == null)
        {
            throw new InvalidEmailAddressOrPasswordException();
        }

        var isPasswordCorrect = passwordHasherService.VerifyPassword(user.Password, password);

        if (!isPasswordCorrect)
        {
            throw new InvalidEmailAddressOrPasswordException();
        }

        var identity = new ClaimsIdentity([new Claim("email", user.EmailAddress)]);
        var claims = new Dictionary<string, string> { ["role"] = user.Role.ToString() };
        var expiration = DateTime.UtcNow.AddDays(1);

        var token = jwtService.GenerateToken(identity, claims, expiration);

        return new JsonWebToken(token);
    }
}
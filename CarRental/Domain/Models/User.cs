// <copyright file="User.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Infrastructure.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public record User(
    string EmailAddress,
    string Password,
    string Name,
    string Surname,
    string PhoneNumber,
    string Pesel)
{
    [BsonId]
    public string Id { get; } = Guid.NewGuid().ToString();

    public ApplicationRoles Role { get; set; } = ApplicationRoles.User;
}
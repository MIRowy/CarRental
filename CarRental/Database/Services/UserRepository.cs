// <copyright file="UserRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class UserRepository(IMongoDatabase database) : IUserRepository
{
    private readonly IMongoCollection<User> collection = database.GetCollection<User>("Users");

    public Task Add(User user) => this.collection.InsertOneAsync(user);

    public Task<User> Get(string emailAddress) =>
        this.collection.Find(user => user.EmailAddress == emailAddress).FirstOrDefaultAsync();
}
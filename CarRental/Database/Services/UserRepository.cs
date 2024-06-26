﻿// <copyright file="UserRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<User>("Users");

        SetupIndexes();
    }

    public Task Add(User user) => _collection.InsertOneAsync(user);

    public Task<User> Get(string emailAddress) =>
        _collection.Find(user => user.EmailAddress == emailAddress).FirstOrDefaultAsync();

    public Task<List<User>> GetAll() =>
        _collection.Find(_ => true).ToListAsync();

    public Task<User> Update(string emailAddress, UpdateDefinition<User> updateDefinition) =>
        _collection.FindOneAndUpdateAsync(a => a.EmailAddress == emailAddress, updateDefinition);

    public Task Delete(string emailAddress) =>
        _collection.DeleteOneAsync(a => a.EmailAddress == emailAddress);

    private void SetupIndexes()
    {
        var indexKeysDefinition = Builders<User>.IndexKeys.Ascending(a => a.EmailAddress);
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<User>(indexKeysDefinition, indexOptions);

        _collection.Indexes.CreateOne(indexModel);
    }
}
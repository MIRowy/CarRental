// <copyright file="UserRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> collection;

    public UserRepository(IMongoDatabase database)
    {
        this.collection = database.GetCollection<User>("Users");

        this.SetupIndexes();
    }

    public Task Add(User user) => this.collection.InsertOneAsync(user);

    public Task<User> Get(string emailAddress) =>
        this.collection.Find(user => user.EmailAddress == emailAddress).FirstOrDefaultAsync();

    public Task<List<User>> GetAll() =>
        this.collection.Find(_ => true).ToListAsync();

    public Task<User> Update(string emailAddress, User user) =>
        this.collection.FindOneAndReplaceAsync(a => a.EmailAddress == emailAddress, user);

    public Task Delete(string emailAddress) =>
        this.collection.DeleteOneAsync(a => a.EmailAddress == emailAddress);

    private void SetupIndexes()
    {
        var indexKeysDefinition = Builders<User>.IndexKeys.Ascending(a => a.EmailAddress);
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<User>(indexKeysDefinition, indexOptions);

        this.collection.Indexes.CreateOne(indexModel);
    }
}
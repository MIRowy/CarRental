// <copyright file="CarRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class CarRepository(IMongoDatabase database) : ICarRepository
{
    private readonly IMongoCollection<Car> collection = database.GetCollection<Car>("Cars");

    public Task Add(Car car) => this.collection.InsertOneAsync(car);

    public Task<Car> Get(string id) => this.collection.Find(a => a.Id == id).FirstOrDefaultAsync();

    public Task<List<Car>> GetAll() => this.collection.Find(_ => true).ToListAsync();

    public Task<Car> Update(string id, UpdateDefinition<Car> updateDefinition) =>
        this.collection.FindOneAndUpdateAsync(a => a.Id == id, updateDefinition);

    public Task Delete(string id) => this.collection.DeleteOneAsync(a => a.Id == id);
}
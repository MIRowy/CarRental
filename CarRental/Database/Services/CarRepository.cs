// <copyright file="CarRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class CarRepository(IMongoDatabase database) : ICarRepository
{
    private readonly IMongoCollection<Car> _collection = database.GetCollection<Car>("Cars");

    public Task Add(Car car) => _collection.InsertOneAsync(car);

    public Task<Car> Get(string id) => _collection.Find(a => a.Id == id).FirstOrDefaultAsync();

    public Task<List<Car>> GetAll() => _collection.Find(_ => true).ToListAsync();

    public Task<Car> Update(string id, UpdateDefinition<Car> updateDefinition) =>
        _collection.FindOneAndUpdateAsync(a => a.Id == id, updateDefinition);

    public Task Delete(string id) => _collection.DeleteOneAsync(a => a.Id == id);
}
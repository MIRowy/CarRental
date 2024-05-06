// <copyright file="CarFailureRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class CarFailureRepository(IMongoDatabase database) : ICarFailureRepository
{
    private readonly IMongoCollection<CarFailure> _collection = database.GetCollection<CarFailure>("CarFailures");

    public Task Add(CarFailure carFailure) => _collection.InsertOneAsync(carFailure);

    public Task<CarFailure> Get(string id) => _collection.Find(a => a.Id == id).FirstOrDefaultAsync();

    public Task<List<CarFailure>> GetAll() => _collection.Find(_ => true).ToListAsync();

    public Task<CarFailure> Update(string id, UpdateDefinition<CarFailure> updateDefinition) =>
        _collection.FindOneAndUpdateAsync(a => a.Id == id, updateDefinition);

    public Task Delete(string id) => _collection.DeleteOneAsync(a => a.Id == id);
}
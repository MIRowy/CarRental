// <copyright file="CarReturnRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class CarReturnRepository(IMongoDatabase database) : ICarReturnRepository
{
    private readonly IMongoCollection<CarReturn> collection = database.GetCollection<CarReturn>("CarReturns");

    public Task Add(CarReturn carReturn) => this.collection.InsertOneAsync(carReturn);

    public Task<CarReturn> Get(string id) => this.collection.Find(a => a.Id == id).FirstOrDefaultAsync();

    public Task<List<CarReturn>> GetAll() => this.collection.Find(_ => true).ToListAsync();

    public Task<CarReturn> Update(string id, UpdateDefinition<CarReturn> updateDefinition) =>
        this.collection.FindOneAndUpdateAsync(a => a.Id == id, updateDefinition);

    public Task Delete(string id) => this.collection.DeleteOneAsync(id);
}
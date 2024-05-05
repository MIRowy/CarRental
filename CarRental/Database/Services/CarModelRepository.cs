// <copyright file="CarModelRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class CarModelRepository(IMongoDatabase database) : ICarModelRepository
{
    private readonly IMongoCollection<CarModel> collection = database.GetCollection<CarModel>("CarModels");

    public Task Add(CarModel model) => this.collection.InsertOneAsync(model);

    public Task<CarModel> Get(string id) => this.collection.Find(a => a.Id == id).FirstOrDefaultAsync();

    public Task<List<CarModel>> GetAll() => this.collection.Find(_ => true).ToListAsync();

    public Task<CarModel> Update(string id, UpdateDefinition<CarModel> updateDefinition) =>
        this.collection.FindOneAndUpdateAsync(a => a.Id == id, updateDefinition);

    public Task Delete(string id) => this.collection.DeleteOneAsync(a => a.Id == id);
}
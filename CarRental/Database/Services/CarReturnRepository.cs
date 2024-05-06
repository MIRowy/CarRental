// <copyright file="CarReturnRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class CarReturnRepository(IMongoDatabase database) : ICarReturnRepository
{
    private readonly IMongoCollection<CarReturn> _collection = database.GetCollection<CarReturn>("CarReturns");

    public Task Add(CarReturn carReturn) => _collection.InsertOneAsync(carReturn);

    public Task<CarReturn> Get(string userId, string id) =>
        _collection.Find(a => a.Id == id && a.CarRent.CarReservation.UserId == userId).FirstOrDefaultAsync();

    public Task<List<CarReturn>> GetAll() => _collection.Find(_ => true).ToListAsync();

    public Task<CarReturn> Update(string id, UpdateDefinition<CarReturn> updateDefinition) =>
        _collection.FindOneAndUpdateAsync(a => a.Id == id, updateDefinition);

    public Task Delete(string id) => _collection.DeleteOneAsync(id);
}
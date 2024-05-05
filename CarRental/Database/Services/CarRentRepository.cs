// <copyright file="CarRentRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class CarRentRepository(IMongoDatabase database) : ICarRentRepository
{
    private readonly IMongoCollection<CarRent> collection = database.GetCollection<CarRent>("CarRents");

    public Task Add(CarRent carRent) => this.collection.InsertOneAsync(carRent);

    public Task<CarRent> Get(string userId, string id) =>
        this.collection.Find(a => a.Id == id && a.CarReservation.UserId == userId).FirstOrDefaultAsync();

    public Task<List<CarRent>> GetAll() => this.collection.Find(_ => true).ToListAsync();

    public Task<CarRent> Update(string id, UpdateDefinition<CarRent> updateDefinition) =>
        this.collection.FindOneAndUpdateAsync(a => a.Id == id, updateDefinition);

    public Task Delete(string id) => this.collection.DeleteOneAsync(a => a.Id == id);
}
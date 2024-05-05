// <copyright file="CarReservationRepository.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Models;
using MongoDB.Driver;

namespace CarRental.Database.Services;

public class CarReservationRepository(IMongoDatabase database) : ICarReservationRepository
{
    private readonly IMongoCollection<CarReservation> collection = database.GetCollection<CarReservation>("CarReservations");

    public Task Add(CarReservation carReservation) => this.collection.InsertOneAsync(carReservation);

    public Task<CarReservation> Get(string id, string userId) =>
        this.collection.Find(a => a.Id == id && a.UserId == userId).FirstOrDefaultAsync();

    public Task<List<CarReservation>> GetAll() => this.collection.Find(_ => true).ToListAsync();

    public Task<List<CarReservation>> GetForCarModelIdBetweenDates(
        string carModelId,
        DateTime startDate,
        DateTime endDate) =>
        this.collection.Find(a => a.Car.Model.Id == carModelId
                                  && a.End > startDate
                                  && a.Start < endDate).ToListAsync();

    public Task<CarReservation> Update(string id, UpdateDefinition<CarReservation> updateDefinition) =>
        this.collection.FindOneAndUpdateAsync(a => a.Id == id, updateDefinition);

    public Task Delete(string id) => this.collection.DeleteOneAsync(a => a.Id == id);

    public async Task<bool> IsCarAvailableForReservation(string carId, DateTime startDate, DateTime endDate)
    {
        var documentCount = await this.collection
            .CountDocumentsAsync(a => a.Car.Id == carId
                                      && (a.End < startDate || a.Start > endDate));

        return documentCount > 0;
    }

    public async Task<bool> Is24HBeforeCollection(string carReservationId)
    {
        var todayPlus24H = DateTime.Now.AddDays(1);
        var documentCount = await this.collection
            .CountDocumentsAsync(a => a.Id == carReservationId && a.Start < todayPlus24H);

        return documentCount > 0;
    }
}
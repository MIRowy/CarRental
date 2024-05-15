// <copyright file="CarReturn.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public record CarReturn
{
    public CarReturn(CarRent carRent, DateTime date, bool isCleaningNeeded, int lackingGas = 0)
    {
        this.CarRent = carRent;
        this.Date = date;
        this.IsCleaningNeeded = isCleaningNeeded;
        this.LackingGas = lackingGas;

        this.TotalPrice = double.Round(this.GetTotalPrice(), 2);
    }

    [BsonId]
    [BsonElement("_id")]
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public CarRent CarRent { get; init; }

    public DateTime Date { get; init; }

    public bool IsCleaningNeeded { get; init; }

    public int LackingGas { get; init; }

    public double TotalPrice { get; init; }

    private double GetTotalPrice()
    {
        var totalPrice = CarRent.CarReservation.TotalPrice + GetPriceForOvertime();

        if (IsCleaningNeeded)
        {
            totalPrice += 150;
        }

        totalPrice += LackingGas * 5.19;

        return totalPrice;
    }

    private double GetPriceForOvertime()
    {
        return (Date - CarRent.CarReservation.End).Days * CarRent.CarReservation.Car.PricePerDay * 1.5;
    }
}
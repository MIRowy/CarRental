// <copyright file="CarReservation.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using MongoDB.Bson.Serialization.Attributes;

namespace CarRental.Domain.Models;

public class CarReservation
{
    public CarReservation(string userId, Car car, DateTime start, DateTime end, bool isDepositPaid)
    {
        UserId = userId;
        Car = car;
        Start = start;
        End = end;
        IsDepositPaid = isDepositPaid;

        this.TotalPrice = double.Round(this.GetTotalPrice(), 2);
    }

    [BsonId]
    [BsonElement("_id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserId { get; init; }

    public Car Car { get; init; }

    public DateTime Start { get; init; }

    public DateTime End { get; init; }

    public bool IsDepositPaid { get; init; }

    public double TotalPrice { get; init; }

    private double GetTotalPrice()
    {
        if (IsDepositPaid)
        {
            return (Car.PricePerDay * GetLengthInDays()) - GetDeposit();
        }

        return Car.PricePerDay * GetLengthInDays();
    }

    private int GetLengthInDays()
    {
        return (End - Start).Days;
    }

    private double GetDeposit()
    {
        return Car.PricePerDay * GetLengthInDays() * 0.25;
    }
}
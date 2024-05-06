// <copyright file="UpdateDefinitionBuilderTests.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Domain.Dto;
using CarRental.Domain.Enums;
using CarRental.Domain.Models;
using CarRental.Domain.Utils;
using FluentAssertions;
using FluentAssertions.Execution;
using MongoDB.Bson.Serialization;

namespace CarRental.UnitTests.Domain.Utils;

public class UpdateDefinitionBuilderTests
{
    [Fact]
    public void UpdateDefinitionBuilder_ConvertsUpdateCarDtoToUpdateDefinition_CorrectUpdateDefinition()
    {
        // Arrange
        const string id = nameof(id);
        const string carModelId = nameof(carModelId);
        const string description = nameof(description);
        const int odometer = 0;

        // Act
        var dto = new UpdateCarDto(id, carModelId, description, odometer);
        var updateDefinition = UpdateDefinitionBuilder.Build<UpdateCarDto, Car>(dto);
        var bsonValue = updateDefinition.Render(
            BsonSerializer.SerializerRegistry.GetSerializer<Car>(),
            BsonSerializer.SerializerRegistry);

        // Assert
        using (new AssertionScope())
        {
            bsonValue[0]["_id"].Should().Be(id);
            bsonValue[0]["Description"].Should().Be(description);
            bsonValue[0]["Odometer"].Should().Be(odometer);
        }
    }

    [Fact]
    public void UpdateDefinitionBuilder_ConvertsUpdateCarModelDtoToUpdateDefinition_CorrectUpdateDefinition()
    {
        // Arrange
        const string id = nameof(id);
        const string brand = nameof(brand);
        const string variant = nameof(variant);
        const int engine = 100;
        const int power = 100;
        const string colour = "red";
        const GearboxTypes gearbox = GearboxTypes.Manual;

        // Act
        var dto = new UpdateCarModelDto(id, brand, variant, engine, power, colour, gearbox);
        var updateDefinition = UpdateDefinitionBuilder.Build<UpdateCarModelDto, CarModel>(dto);
        var bsonValue = updateDefinition.Render(
            BsonSerializer.SerializerRegistry.GetSerializer<CarModel>(),
            BsonSerializer.SerializerRegistry);

        // Assert
        using (new AssertionScope())
        {
            bsonValue[0]["_id"].Should().Be(id);
            bsonValue[0]["Brand"].Should().Be(brand);
            bsonValue[0]["Variant"].Should().Be(variant);
            bsonValue[0]["Engine"].Should().Be(engine);
            bsonValue[0]["Power"].Should().Be(power);
            bsonValue[0]["Colour"].Should().Be(colour);
            bsonValue[0]["Gearbox"].Should().Be(gearbox);
        }
    }

    [Fact]
    public void UpdateDefinitionBuilder_ConvertsUpdateCarReservationDtoToUpdateDefinition_CorrectUpdateDefinition()
    {
        // Arrange
        const string carReservationId = nameof(carReservationId);
        const string carId = nameof(carId);
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(7);

        // Act
        var dto = new UpdateCarReservationDto(carReservationId, carId, startDate, endDate);
        var updateDefinition = UpdateDefinitionBuilder.Build<UpdateCarReservationDto, CarReservation>(dto);
        var bsonValue = updateDefinition.Render(
            BsonSerializer.SerializerRegistry.GetSerializer<CarReservation>(),
            BsonSerializer.SerializerRegistry);

        // Assert
        using (new AssertionScope())
        {
            bsonValue[0]["_id"].Should().Be(carReservationId);
            bsonValue[0]["Start"].Should().Be(startDate);
            bsonValue[0]["End"].Should().Be(endDate);
        }
    }
}
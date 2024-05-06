// <copyright file="CarModelServiceTests.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Dto;
using CarRental.Domain.Enums;
using CarRental.Domain.Models;
using CarRental.Domain.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using MongoDB.Driver;
using Moq;

namespace CarRental.UnitTests.Domain.Services;

public class CarModelServiceTests
{
    private readonly Mock<ICarModelRepository> _mockCarModelRepository = new();

    private readonly CarModelService _service;

    public CarModelServiceTests()
    {
        _service = new CarModelService(_mockCarModelRepository.Object);
    }

    [Fact]
    public async Task Add_WorksCorrectly_NewCar()
    {
        // Arrange
        const string brand = nameof(brand);
        const string variant = nameof(variant);
        const int engine = 100;
        const int power = 100;
        const string colour = "red";
        const GearboxTypes gearbox = GearboxTypes.Manual;

        // Act
        var dto = new AddCarModelDto(brand, variant, engine, power, colour, gearbox);
        var result = await _service.Add(dto);

        // Assert
        using (new AssertionScope())
        {
            _mockCarModelRepository.Verify(a => a.Add(It.IsAny<CarModel>()), Times.Once);

            result.Id.Should().NotBeNullOrEmpty();
            result.Brand.Should().Be(brand);
            result.Variant.Should().Be(variant);
            result.Engine.Should().Be(engine);
            result.Power.Should().Be(power);
            result.Colour.Should().Be(colour);
            result.Gearbox.Should().Be(gearbox);
        }
    }

    [Fact]
    public async Task Update_WorksCorrectly_UpdatedCarModel()
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

        await _service.Update(dto);

        // Assert
        using (new AssertionScope())
        {
            _mockCarModelRepository.Verify(a => a.Update(id, It.IsAny<UpdateDefinition<CarModel>>()));
        }
    }
}
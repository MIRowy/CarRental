// <copyright file="CarServiceUnitTests.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using AutoFixture;
using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Dto;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.Domain.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using MongoDB.Driver;
using Moq;

namespace CarRental.UnitTests.Domain.Services;

public class CarServiceUnitTests
{
    private readonly Mock<ICarModelRepository> _mockCarModelRepository = new();
    private readonly Mock<ICarRepository> _mockCarRepository = new();

    private readonly CarService _service;

    public CarServiceUnitTests()
    {
        _service = new CarService(_mockCarModelRepository.Object, _mockCarRepository.Object);
    }

    [Fact]
    public async Task Add_Successful_ReturnsCar()
    {
        // Arrange
        const string carModelId = nameof(carModelId);
        const string description = nameof(description);
        const int odometer = 12000;
        const int pricePerDay = 100;

        var fixture = new Fixture();
        var carModel = fixture.Create<CarModel>();
        var car = new Car(carModel, description, odometer, pricePerDay);

        _mockCarModelRepository.Setup(repo => repo.Get(carModelId)).ReturnsAsync(carModel);
        _mockCarRepository.Setup(repo => repo.Add(It.IsAny<Car>())).Returns(Task.CompletedTask);

        // Act
        var dto = new AddCarDto(carModelId, description, odometer, pricePerDay);
        var result = await _service.Add(dto);

        car = car with { Id = result.Id };

        // Assert
        using (new AssertionScope())
        {
            result.Should().BeEquivalentTo(car, options => options.ComparingByMembers<Car>());

            _mockCarRepository.Verify(repo => repo.Add(It.IsAny<Car>()), Times.Once);
        }
    }

    [Fact]
    public async Task Add_CarModelNotFound_ThrowsCarModelNotFoundException()
    {
        // Arrange
        const string carModelId = nameof(carModelId);
        const string description = nameof(description);
        const int odometer = 12000;
        const int pricePerDay = 100;

        _mockCarModelRepository.Setup(repo => repo.Get(carModelId)).ReturnsAsync((CarModel)null!);

        // Act
        var dto = new AddCarDto(carModelId, description, odometer, pricePerDay);
        var action = () => _service.Add(dto);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CarModelNotFoundException>();

            _mockCarModelRepository.Verify(repo => repo.Get(dto.CarModelId), Times.Once);
        }
    }

    [Fact]
    public async Task Update_Successful_ReturnsUpdatedCar()
    {
        // Arrange
        const string id = nameof(id);
        const string carModelId = nameof(carModelId);
        const string description = nameof(description);
        const int odometer = 12000;
        const int pricePerDay = 100;

        var fixture = new Fixture();
        var carModel = fixture.Create<CarModel>();
        var updatedCar = new Car(carModel, description, odometer, pricePerDay);

        _mockCarRepository
            .Setup(repo => repo.Update(id, It.IsAny<UpdateDefinition<Car>>()))
            .ReturnsAsync(updatedCar);

        // Act
        var dto = new UpdateCarDto(id, carModelId, description, odometer, pricePerDay);
        var result = await _service.Update(dto);

        // Assert
        using (new AssertionScope())
        {
            result.Should().BeEquivalentTo(updatedCar, options => options.ComparingByMembers<Car>());

            _mockCarRepository.Verify(repo => repo.Update(dto.Id, It.IsAny<UpdateDefinition<Car>>()), Times.Once);
        }
    }
}
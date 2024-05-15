// <copyright file="CarRentServiceTests.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using AutoFixture;
using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Dto;
using CarRental.Domain.Enums;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.Domain.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using MongoDB.Driver;
using Moq;

namespace CarRental.UnitTests.Domain.Services;

public class CarRentServiceTests
{
    private readonly Mock<ICarRentRepository> _mockCarRentRepository = new();
    private readonly Mock<ICarReservationRepository> _mockCarReservationRepository = new();
    private readonly Mock<ICarFailureRepository> _mockCarFailureRepository = new();
    private readonly Mock<ICarReturnRepository> _mockCarReturnRepository = new();

    private readonly CarRentService _service;

    public CarRentServiceTests()
    {
        _service = new CarRentService(
            _mockCarRentRepository.Object,
            _mockCarReservationRepository.Object,
            _mockCarFailureRepository.Object,
            _mockCarReturnRepository.Object);
    }

    [Fact]
    public async Task Add_CorrectParameters_NewCarRent()
    {
        // Arrange
        const string userId = nameof(userId);
        const string carReservationId = nameof(carReservationId);

        var fixture = new Fixture();

        var car = fixture.Create<Car>();

        var carReservation = new CarReservation(userId, car, DateTime.Now, DateTime.Now, false)
        {
            Id = carReservationId,
        };

        _mockCarReservationRepository.Setup(a => a.Get(carReservationId)).ReturnsAsync(carReservation);

        // Act
        var dto = new AddCarRentDto(carReservationId);
        var result = await _service.Add(dto);

        // Assert
        using (new AssertionScope())
        {
            _mockCarReservationRepository.Verify(a => a.Get(carReservationId), Times.Once);
            _mockCarRentRepository.Verify(a => a.Add(It.Is<CarRent>(b => b.Id == result.Id)), Times.Once);

            result.Id.Should().NotBeNullOrEmpty();
            result.CarReservation.Id.Should().Be(carReservationId);
            result.CarReservation.UserId.Should().Be(userId);
        }
    }

    [Fact]
    public async Task Add_ReservationNotFound_ThrowsReservationNotFoundException()
    {
        // Arrange
        const string carReservationId = nameof(carReservationId);

        // Act
        var dto = new AddCarRentDto(carReservationId);
        var action = () => _service.Add(dto);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CarReservationNotFoundException>();

            _mockCarReservationRepository.Verify(a => a.Get(carReservationId), Times.Once);
        }
    }

    [Fact]
    public async Task CreateFailure_CorrectParameters_NewCarFailure()
    {
        // Arrange
        const string userId = nameof(userId);
        const string carRentId = nameof(carRentId);
        const string description = nameof(description);

        var fixture = new Fixture();

        var car = fixture.Create<Car>();

        var carRent = new CarRent(new CarReservation(userId, car, DateTime.Now, DateTime.Now, false))
        {
            Id = carRentId,
        };

        _mockCarRentRepository.Setup(a => a.Get(userId, carRentId)).ReturnsAsync(carRent);

        // Act
        var dto = new CreateFailureDto(carRentId, description);
        var result = await _service.CreateFailure(userId, dto);

        // Assert
        using (new AssertionScope())
        {
            _mockCarRentRepository.Verify(a => a.Get(userId, carRentId), Times.Once);
            _mockCarFailureRepository.Verify(a => a.Add(It.Is<CarFailure>(b => b.Id == result.Id)), Times.Once);

            result.CarRent.Id.Should().Be(carRentId);
            result.CarRent.CarReservation.UserId.Should().Be(userId);
            result.Description.Should().Be(description);
        }
    }

    [Fact]
    public async Task CreateFailure_RentNotFound_ThrowsCarRentNotFoundException()
    {
        // Arrange
        const string userId = nameof(userId);
        const string carRentId = nameof(carRentId);
        const string description = nameof(description);

        // Act
        var dto = new CreateFailureDto(carRentId, description);
        var action = () => _service.CreateFailure(userId, dto);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CarRentNotFoundException>();

            _mockCarRentRepository.Verify(a => a.Get(userId, carRentId), Times.Once);
        }
    }

    [Fact]
    public async Task CompleteRent_CorrectParameters_NewCarReturn()
    {
        // Arrange
        const string userId = nameof(userId);
        const string carRentId = nameof(carRentId);
        var returnDate = DateTime.Now;
        const bool isCleaningNeeded = false;
        const int lackingGas = 100;

        var fixture = new Fixture();

        var car = fixture.Create<Car>();

        var carRent = new CarRent(new CarReservation(userId, car, DateTime.Now, DateTime.Now, false))
        {
            Id = carRentId,
        };

        _mockCarRentRepository.Setup(a => a.Get(carRentId)).ReturnsAsync(carRent);
        _mockCarRentRepository
            .Setup(a => a.Update(carRentId, It.IsAny<UpdateDefinition<CarRent>>()))
            .ReturnsAsync(carRent with { Status = CarRentStatuses.Completed });

        // Act
        var dto = new CompleteCarRentDto(carRentId, returnDate, isCleaningNeeded, lackingGas);
        var result = await _service.CompleteRent(dto);

        // Assert
        using (new AssertionScope())
        {
            _mockCarRentRepository.Verify(a => a.Get(carRentId), Times.Once);
            _mockCarReturnRepository.Verify(a => a.Add(It.Is<CarReturn>(b => b.Id == result.Id)), Times.Once);

            result.CarRent.Id.Should().Be(carRentId);
            result.CarRent.CarReservation.UserId.Should().Be(userId);
            result.Date.Should().Be(returnDate);
            result.IsCleaningNeeded.Should().Be(isCleaningNeeded);
            result.LackingGas.Should().Be(lackingGas);
            result.CarRent.Status.Should().Be(CarRentStatuses.Completed);
        }
    }

    [Fact]
    public async Task CompleteRent_RentNotFound_ThrowsCarRentNotFoundException()
    {
        // Arrange
        const string carRentId = nameof(carRentId);
        var returnDate = DateTime.Now;
        const bool isCleaningNeeded = false;
        const int lackingGas = 100;

        _mockCarRentRepository.Setup(a => a.Get(carRentId)).ReturnsAsync((CarRent)null!);

        // Act
        var dto = new CompleteCarRentDto(carRentId, returnDate, isCleaningNeeded, lackingGas);
        var action = () => _service.CompleteRent(dto);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CarRentNotFoundException>();

            _mockCarRentRepository.Verify(a => a.Get(carRentId), Times.Once);
        }
    }
}
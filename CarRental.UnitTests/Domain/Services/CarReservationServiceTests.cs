// <copyright file="CarReservationServiceTests.cs" company="Car Rental Inc">
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

public class CarReservationServiceTests
{
    private readonly Mock<ICarRepository> _mockCarRepository = new();
    private readonly Mock<ICarReservationRepository> _mockCarReservationRepository = new();

    private readonly CarReservationService _service;

    public CarReservationServiceTests()
    {
        _service = new CarReservationService(_mockCarRepository.Object, _mockCarReservationRepository.Object);
    }

    [Fact]
    public async Task Add_Successful_ReturnsCarReservation()
    {
        // Arrange
        const string userId = nameof(userId);
        const string carId = nameof(carId);
        var startDate = DateTime.Now.AddDays(1);
        var endDate = startDate.AddDays(7);
        const bool isDepositPaid = false;

        var fixture = new Fixture();

        var car = fixture.Create<Car>();

        var carReservation = new CarReservation(userId, car, startDate, endDate, isDepositPaid);

        _mockCarRepository.Setup(repo => repo.Get(carId)).ReturnsAsync(car);
        _mockCarReservationRepository
            .Setup(repo => repo.IsCarAvailableForReservation(carId, startDate, endDate))
            .ReturnsAsync(true);

        _mockCarReservationRepository.Setup(repo => repo.Add(carReservation)).Returns(Task.CompletedTask);

        // Act
        var dto = new AddCarReservationDto(carId, startDate, endDate, isDepositPaid);
        var result = await _service.Add(userId, dto);

        carReservation = carReservation with { Id = result.Id };

        // Assert
        using (new AssertionScope())
        {
            result.Should().BeEquivalentTo(carReservation, options => options.ComparingByMembers<CarReservation>());

            _mockCarReservationRepository.Verify(repo => repo.Add(It.IsAny<CarReservation>()), Times.Once);
        }
    }

    [Fact]
    public async Task Add_CarNotFound_ThrowsCarNotFoundException()
    {
        // Arrange
        const string userId = nameof(userId);
        const string carId = nameof(carId);
        var startDate = DateTime.Now.AddDays(1);
        var endDate = startDate.AddDays(7);
        const bool isDepositPaid = false;

        _mockCarRepository.Setup(a => a.Get(carId)).ReturnsAsync((Car)null!);

        // Act
        var dto = new AddCarReservationDto(carId, startDate, endDate, isDepositPaid);
        var action = () => _service.Add(userId, dto);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CarNotFoundException>();

            _mockCarRepository.Verify(a => a.Get(dto.CarId), Times.Once);
        }
    }

    [Fact]
    public async Task Add_CarNotAvailable_ThrowsCarNotAvailableException()
    {
        // Arrange
        const string userId = nameof(userId);
        const string carId = nameof(carId);
        var startDate = DateTime.Now.AddDays(1);
        var endDate = startDate.AddDays(7);
        const bool isDepositPaid = false;

        var fixture = new Fixture();

        var car = fixture.Create<Car>();

        _mockCarRepository.Setup(a => a.Get(carId)).ReturnsAsync(car);
        _mockCarReservationRepository
            .Setup(a => a.IsCarAvailableForReservation(carId, startDate, endDate))
            .ReturnsAsync(false);

        // Act
        var dto = new AddCarReservationDto(carId, startDate, endDate, isDepositPaid);
        var action = () => _service.Add(userId, dto);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CarNotAvailableException>();

            _mockCarReservationRepository.Verify(
                repo => repo.IsCarAvailableForReservation(dto.CarId, dto.StartDate, dto.EndDate),
                Times.Once);
        }
    }

    [Fact]
    public async Task GetForCarModelIdBetweenDates_ReturnsCorrectReservationsDtoList()
    {
        // Arrange
        const string carModelId = nameof(carModelId);
        var startDate = DateTime.Now.AddDays(1);
        var endDate = startDate.AddDays(5);

        var fixture = new Fixture();
        var carReservations = fixture.CreateMany<CarReservation>(3).ToList();

        _mockCarReservationRepository
            .Setup(a => a.GetForCarModelIdBetweenDates(carModelId, startDate, endDate))
            .ReturnsAsync(carReservations);

        var expectedDtos = carReservations
            .Select(res => new GetCarReservationsDto(res.Start, res.End))
            .ToList();

        // Act
        var result = await _service.GetForCarModelIdBetweenDates(carModelId, startDate, endDate);

        // Assert
        using (new AssertionScope())
        {
            result.Should().BeEquivalentTo(expectedDtos);

            _mockCarReservationRepository.Verify(
                a => a.GetForCarModelIdBetweenDates(carModelId, startDate, endDate),
                Times.Once);
        }
    }

    [Fact]
    public async Task Update_Successful_ReturnsUpdatedReservation()
    {
        // Arrange
        const string userId = nameof(userId);
        const string id = nameof(id);
        const string carId = nameof(carId);
        var start = DateTime.Now;
        var end = start.AddDays(7);

        var fixture = new Fixture();

        var carReservation = fixture.Create<CarReservation>();
        var car = fixture.Create<Car>();
        var updatedReservation = carReservation with { Car = car, Start = start, End = end };

        _mockCarReservationRepository.Setup(a => a.Get(userId, id)).ReturnsAsync(carReservation);
        _mockCarRepository.Setup(a => a.Get(carId)).ReturnsAsync(car);
        _mockCarReservationRepository.Setup(a => a.IsCarAvailableForReservation(carId, start, end)).ReturnsAsync(true);
        _mockCarReservationRepository
            .Setup(a => a.Update(id, It.IsAny<UpdateDefinition<CarReservation>>()))
            .ReturnsAsync(updatedReservation);

        // Act
        var dto = new UpdateCarReservationDto(id, carId, start, end);
        var result = await _service.Update(userId, dto);

        // Assert
        using (new AssertionScope())
        {
            result.Should().Be(updatedReservation);

            _mockCarReservationRepository.Verify(
                a => a.Update(dto.Id, It.IsAny<UpdateDefinition<CarReservation>>()),
                Times.Once);
        }
    }

    [Fact]
    public async Task Update_CarReservationNotFound_ThrowsCarReservationNotFoundException()
    {
        // Arrange
        const string userId = nameof(userId);
        const string id = nameof(id);
        const string carId = nameof(carId);
        var start = DateTime.Now;
        var end = start.AddDays(7);

        _mockCarReservationRepository.Setup(a => a.Get(userId, id)).ReturnsAsync((CarReservation)null!);

        // Act
        var dto = new UpdateCarReservationDto(id, carId, start, end);
        var action = () => _service.Update(userId, dto);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CarReservationNotFoundException>();

            _mockCarReservationRepository.Verify(a => a.Get(userId, id), Times.Once);
        }
    }

    [Fact]
    public async Task Update_CarNotFound_ThrowsCarNotFoundException()
    {
        // Arrange
        const string userId = nameof(userId);
        const string id = nameof(id);
        const string carId = nameof(carId);
        var start = DateTime.Now;
        var end = start.AddDays(7);

        var carReservation = new Fixture().Create<CarReservation>();

        _mockCarReservationRepository.Setup(a => a.Get(userId, id)).ReturnsAsync(carReservation);
        _mockCarRepository.Setup(a => a.Get(carId)).ReturnsAsync((Car)null!);

        // Act
        var dto = new UpdateCarReservationDto(id, carId, start, end);
        var action = () => _service.Update(userId, dto);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CarNotFoundException>();

            _mockCarRepository.Verify(a => a.Get(carId), Times.Once);
        }
    }

    [Fact]
    public async Task Update_CarNotAvailable_ThrowsCarNotAvailableException()
    {
        // Arrange
        const string userId = nameof(userId);
        const string id = nameof(id);
        const string carId = nameof(carId);
        var start = DateTime.Now;
        var end = start.AddDays(7);

        var fixture = new Fixture();

        var carReservation = fixture.Create<CarReservation>();
        var car = fixture.Create<Car>();

        _mockCarReservationRepository.Setup(a => a.Get(userId, id)).ReturnsAsync(carReservation);
        _mockCarRepository.Setup(a => a.Get(carId)).ReturnsAsync(car);
        _mockCarReservationRepository
            .Setup(a => a.IsCarAvailableForReservation(carId, start, end))
            .ReturnsAsync(false);

        // Act
        var dto = new UpdateCarReservationDto(id, carId, start, end);
        var action = () => _service.Update(userId, dto);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CarNotAvailableException>();

            _mockCarReservationRepository.Verify(a => a.IsCarAvailableForReservation(carId, start, end), Times.Once);
        }
    }

    [Fact]
    public async Task Delete_MoreThan24HoursBeforeCollection_DeletesSuccessfully()
    {
        // Arrange
        const string reservationId = nameof(reservationId);

        _mockCarReservationRepository.Setup(a => a.Is24HBeforeCollection(reservationId)).ReturnsAsync(false);

        // Act
        await _service.Delete(reservationId);

        // Assert
        using (new AssertionScope())
        {
            _mockCarReservationRepository.Verify(a => a.Is24HBeforeCollection(reservationId), Times.Once);
            _mockCarReservationRepository.Verify(a => a.Delete(reservationId), Times.Once);
        }
    }

    [Fact]
    public async Task Delete_LessThan24HoursBeforeCollection_ThrowsCannotDeleteReservationException()
    {
        // Arrange
        const string reservationId = nameof(reservationId);

        _mockCarReservationRepository.Setup(a => a.Is24HBeforeCollection(reservationId)).ReturnsAsync(true);

        // Act
        var action = () => _service.Delete(reservationId);

        // Assert
        using (new AssertionScope())
        {
            await action.Should().ThrowExactlyAsync<CannotDeleteReservation24HBeforeCollectionException>();

            _mockCarReservationRepository.Verify(a => a.Is24HBeforeCollection(reservationId), Times.Once);
        }
    }
}
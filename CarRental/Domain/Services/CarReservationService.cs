// <copyright file="CarReservationService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Dto;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.Domain.Services.Interfaces;
using CarRental.Domain.Utils;
using MongoDB.Driver;

namespace CarRental.Domain.Services;

public class CarReservationService(
    ICarRepository carRepository,
    ICarReservationRepository carReservationRepository)
    : ICarReservationService
{
    public async Task<CarReservation> Add(string userId, AddCarReservationDto dto)
    {
        var car = await carRepository.Get(dto.CarId);

        if (car == null)
        {
            throw new CarNotFoundException();
        }

        var isCarAvailable = await carReservationRepository
            .IsCarAvailableForReservation(dto.CarId, dto.StartDate, dto.EndDate);

        if (!isCarAvailable)
        {
            throw new CarNotAvailableException();
        }

        var carReservation = new CarReservation(
            userId,
            car,
            dto.StartDate,
            dto.EndDate,
            dto.IsDepositPaid);

        await carReservationRepository.Add(carReservation);

        return carReservation;
    }

    public Task<CarReservation> Get(string userId, string id) => carReservationRepository.Get(id, userId);

    public Task<List<CarReservation>> GetAll() => carReservationRepository.GetAll();

    public async Task<List<GetCarReservationsDto>> GetForCarModelIdBetweenDates(
        string carModelId,
        DateTime startDate,
        DateTime endDate)
    {
        var carReservations = await carReservationRepository
            .GetForCarModelIdBetweenDates(carModelId, startDate, endDate);

        var dtos = carReservations
            .ConvertAll<GetCarReservationsDto>(a => new GetCarReservationsDto(a.Start, a.End));

        return dtos;
    }

    public async Task<CarReservation> Update(string userId, UpdateCarReservationDto dto)
    {
        var carReservation = await carReservationRepository.Get(userId, dto.CarReservationId);

        if (carReservation == null)
        {
            throw new CarReservationNotFoundException();
        }

        var car = await carRepository.Get(dto.CarId);

        if (car == null)
        {
            throw new CarNotFoundException();
        }

        var isCarAvailable = await carReservationRepository
            .IsCarAvailableForReservation(dto.CarId, dto.StartDate, dto.EndDate);

        if (!isCarAvailable)
        {
            throw new CarNotAvailableException();
        }

        var updateDefinition = UpdateDefinitionBuilder.Build<UpdateCarReservationDto, CarReservation>(dto);

        updateDefinition.Set(a => a.Car, car);

        var updatedCarReservation = await carReservationRepository.Update(dto.CarReservationId, updateDefinition);

        return updatedCarReservation;
    }

    public async Task Delete(string id)
    {
        var is24BeforeCollection = await carReservationRepository.Is24HBeforeCollection(id);

        if (is24BeforeCollection)
        {
            throw new CannotDeleteReservation24HBeforeCollectionException();
        }

        await carReservationRepository.Delete(id);
    }
}
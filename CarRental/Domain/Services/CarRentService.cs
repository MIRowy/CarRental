// <copyright file="CarRentService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Dto;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.Domain.Services.Interfaces;

namespace CarRental.Domain.Services;

public class CarRentService(
    ICarRentRepository carRentRepository,
    ICarReservationRepository carReservationRepository,
    ICarFailureRepository carFailureRepository,
    ICarReturnRepository carReturnRepository)
    : ICarRentService
{
    public Task<CarRent> Get(string userId, string id) => carRentRepository.Get(userId, id);

    public Task<List<CarRent>> GetAll() => carRentRepository.GetAll();

    public async Task<CarRent> Add(string userId, AddCarRentDto dto)
    {
        var carReservation = await carReservationRepository.Get(userId, dto.CarReservationId);

        if (carReservation == null)
        {
            throw new CarReservationNotFoundException();
        }

        var carRent = new CarRent(carReservation);

        await carRentRepository.Add(carRent);

        return carRent;
    }

    public async Task<CarFailure> CreateFailure(string userId, CreateFailureDto dto)
    {
        var carRent = await carRentRepository.Get(userId, dto.CarRentId);

        if (carRent == null)
        {
            throw new CarRentNotFoundException();
        }

        var carFailure = new CarFailure(
            carRent,
            dto.Description);

        await carFailureRepository.Add(carFailure);

        return carFailure;
    }

    public async Task<CarReturn> CompleteRent(string userId, CompleteCarRentDto dto)
    {
        var carRent = await carRentRepository.Get(userId, dto.CarRentId);

        if (carRent == null)
        {
            throw new CarRentNotFoundException();
        }

        var carReturn = new CarReturn(carRent, dto.Date, dto.IsCleaningNeeded, dto.IsFuelingNeeded);

        await carReturnRepository.Add(carReturn);

        return carReturn;
    }
}
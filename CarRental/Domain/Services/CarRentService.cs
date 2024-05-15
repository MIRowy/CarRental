// <copyright file="CarRentService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Dto;
using CarRental.Domain.Enums;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.Domain.Services.Interfaces;
using CarRental.Domain.Utils;
using MongoDB.Driver;

namespace CarRental.Domain.Services;

public class CarRentService(
    ICarRentRepository carRentRepository,
    ICarReservationRepository carReservationRepository,
    ICarFailureRepository carFailureRepository,
    ICarReturnRepository carReturnRepository)
    : ICarRentService
{
    [ExcludeFromCodeCoverage(Justification = "Simple method pass through, no need to test.")]
    public Task<CarRent> Get(string userId, string id) => carRentRepository.Get(userId, id);

    [ExcludeFromCodeCoverage(Justification = "Simple method pass through, no need to test.")]
    public Task<List<CarRent>> GetAll() => carRentRepository.GetAll();

    public async Task<CarRent> Add(AddCarRentDto dto)
    {
        var carReservation = await carReservationRepository.Get(dto.CarReservationId);

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

    public async Task<CarFailure> CompleteCarFailure(CompleteCarFailureDto dto)
    {
        var carFailure = await carFailureRepository.Get(dto.CarFailureId);

        if (carFailure == null)
        {
            throw new CarFailureNotFoundException();
        }

        var updateDefinition = UpdateDefinitionBuilder.Build<CompleteCarFailureDto, CarFailure>(dto);
        var updatedCarFailure = await carFailureRepository.Update(carFailure.Id, updateDefinition);

        if (dto is { IsAccepted: true, IsFastService: false })
        {
            var carRentUpdateDefinitionBuilder = new UpdateDefinitionBuilder<CarRent>();

            carRentUpdateDefinitionBuilder.Set(a => a.Status, CarRentStatuses.Canceled);

            await carRentRepository.Update(carFailure.CarRent.Id, carRentUpdateDefinitionBuilder.Combine());
        }

        return updatedCarFailure;
    }

    public async Task<CarReturn> CompleteRent(CompleteCarRentDto dto)
    {
        var carRent = await carRentRepository.Get(dto.CarRentId);

        if (carRent == null)
        {
            throw new CarRentNotFoundException();
        }

        var carRentUpdateDefinitionBuilder = new UpdateDefinitionBuilder<CarRent>();

        carRentUpdateDefinitionBuilder.Set(a => a.Status, CarRentStatuses.Completed);

        carRent = await carRentRepository.Update(carRent.Id, carRentUpdateDefinitionBuilder.Combine());

        var carReturn = new CarReturn(carRent, dto.Date, dto.IsCleaningNeeded, dto.LackingGas);

        await carReturnRepository.Add(carReturn);

        return carReturn;
    }
}
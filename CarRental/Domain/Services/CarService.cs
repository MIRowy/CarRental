// <copyright file="CarService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Dto;
using CarRental.Domain.Exceptions;
using CarRental.Domain.Models;
using CarRental.Domain.Services.Interfaces;
using CarRental.Domain.Utils;

namespace CarRental.Domain.Services;

public class CarService(
    ICarModelRepository carModelRepository,
    ICarRepository carRepository)
    : ICarService
{
    public async Task<Car> Add(AddCarDto dto)
    {
        var carModel = await carModelRepository.Get(dto.CarModelId);

        if (carModel == null)
        {
            throw new CarModelNotFoundException();
        }

        var car = new Car(
            carModel,
            dto.Description,
            dto.OdoMeter,
            dto.Images);

        await carRepository.Add(car);

        return car;
    }

    public Task<Car> Get(string id) => carRepository.Get(id);

    public Task<List<Car>> GetAll() => carRepository.GetAll();

    public Task<Car> Update(UpdateCarDto dto)
    {
        var updateDefinition = UpdateDefinitionBuilder.Build<UpdateCarDto, Car>(dto);

        return carRepository.Update(dto.Id, updateDefinition);
    }

    public Task Delete(string id) => carRepository.Delete(id);
}
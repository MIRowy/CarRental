// <copyright file="CarModelService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using CarRental.Database.Services.Interfaces;
using CarRental.Domain.Dto;
using CarRental.Domain.Models;
using CarRental.Domain.Services.Interfaces;
using CarRental.Domain.Utils;

namespace CarRental.Domain.Services;

public class CarModelService(ICarModelRepository carModelRepository) : ICarModelService
{
    public async Task<CarModel> Add(AddCarModelDto dto)
    {
        var carModel = new CarModel(
            dto.Brand,
            dto.Variant,
            dto.Engine,
            dto.Power,
            dto.Colour,
            dto.Gearbox);

        await carModelRepository.Add(carModel);

        return carModel;
    }

    public Task<CarModel> Get(string id) => carModelRepository.Get(id);

    public Task<List<CarModel>> GetAll() => carModelRepository.GetAll();

    public Task<CarModel> Update(UpdateCarModelDto dto)
    {
        var updateDefinition = UpdateDefinitionBuilder.Build<UpdateCarModelDto, CarModel>(dto);

        return carModelRepository.Update(dto.Id, updateDefinition);
    }

    public Task Delete(string id) => carModelRepository.Delete(id);
}
// <copyright file="UpdateDefinitionBuilder.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.Reflection;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace CarRental.Domain.Utils;

public static class UpdateDefinitionBuilder
{
    public static UpdateDefinition<TModel>  Build<TDto, TModel>(TDto dto)
        where TDto : class
    {
        var update = Builders<TModel>.Update;
        var updates = new List<UpdateDefinition<TModel>>();
        var dtoProperties = typeof(TDto).GetProperties();

        foreach (var propertyInfo in dtoProperties)
        {
            var value = propertyInfo.GetValue(dto);

            if (value == null)
            {
                continue;
            }

            // Optionally use a custom attribute to specify the target field name in the model
            var bsonElementAttribute = propertyInfo.GetCustomAttribute<BsonElementAttribute>();
            var fieldName = bsonElementAttribute != null
                ? bsonElementAttribute.ElementName
                : propertyInfo.Name;

            var modelType = typeof(TModel);
            var modelProperty = modelType.GetProperty(propertyInfo.Name) ?? modelType.GetProperty(fieldName);

            if (modelProperty != null)
            {
                updates.Add(update.Set(modelProperty.Name, value));
            }
        }

        return update.Combine(updates);
    }
}
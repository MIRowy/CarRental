// <copyright file="PeselAttribute.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarRental.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field)]
public partial class PeselAttribute() : ValidationAttribute("Provided PESEL number is invalid.")
{
    public override bool IsValid(object? value)
    {
        if (value is not string)
        {
            return false;
        }

        var pesel = value.ToString();

        if (pesel is null)
        {
            return false;
        }

        var regex = PeselRegex();

        return regex.IsMatch(pesel);
    }

    [GeneratedRegex("[0-9]{4}[0-3]{1}[0-9}{1}[0-9]{5}")]
    private static partial Regex PeselRegex();
}
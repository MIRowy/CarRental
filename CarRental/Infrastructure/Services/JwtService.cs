// <copyright file="JwtService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CarRental.Infrastructure.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Infrastructure.Services;

public class JwtService(SecurityKey key) : IJwtService
{
    private const string Issuer = "https://localhost:44388/";
    private const string Audience = "https://localhost:44388/";

    public string? GenerateToken(ClaimsIdentity identity, IDictionary<string, string> claims, DateTime? expiration = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = expiration ?? DateTime.UtcNow.AddHours(1),
            Issuer = Issuer,
            Audience = Audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256),
        };

        foreach (var claim in claims)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(claim.Key, claim.Value));
        }

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    ClockSkew = TimeSpan.Zero,
                },
                out _);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public IDictionary<string, string> DecodeToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var decodedToken = tokenHandler.ReadJwtToken(token);

        return decodedToken.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = true)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(
            token,
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = validateLifetime,
                ClockSkew = TimeSpan.Zero,
            },
            out _);

        return principal;
    }
}
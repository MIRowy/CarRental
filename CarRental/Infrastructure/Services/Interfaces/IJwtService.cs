// <copyright file="IJwtService.cs" company="Car Rental Inc">
// Copyright (c) Car Rental Inc. All rights reserved.
// </copyright>

using System.Security.Claims;

namespace CarRental.Infrastructure.Services.Interfaces;

/// <summary>
/// The interface holding contracts for managing JWT.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Generates a JWT token based on the provided user identity, claims, and optional expiration date.
    /// </summary>
    /// <param name="identity">The user's identity which typically includes the user identifier and possibly other identity attributes.</param>
    /// <param name="claims">A dictionary containing key-value pairs of claims to be included in the token.</param>
    /// <param name="expiration">Optional expiration date of the token. If not provided, a default expiration might be applied.</param>
    /// <returns>A string representing the newly created JWT.</returns>
    string? GenerateToken(ClaimsIdentity identity, IDictionary<string, string> claims, DateTime? expiration = null);

    /// <summary>
    /// Validates a given JWT token for correctness and ensures it is not expired, using the signature and issuer details.
    /// </summary>
    /// <param name="token">The JWT string to validate.</param>
    /// <returns>True if the token is valid, false otherwise.</returns>
    bool ValidateToken(string token);

    /// <summary>
    /// Decodes a JWT token to extract its payload without validating its signature. This should be used only in contexts where authenticity is guaranteed by other means.
    /// </summary>
    /// <param name="token">The JWT string to decode.</param>
    /// <returns>A dictionary containing the decoded claims and values from the JWT.</returns>
    IDictionary<string, string> DecodeToken(string token);

    /// <summary>
    /// Extracts the claims principal from the provided JWT token. This method optionally validates the token lifetime.
    /// </summary>
    /// <param name="token">The JWT string from which to extract the principal.</param>
    /// <param name="validateLifetime">A boolean indicating whether to validate the token's expiration and not before claims during extraction.</param>
    /// <returns>A ClaimsPrincipal extracted from the JWT. Returns null if the token is invalid when lifetime validation is requested.</returns>
    ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = true);
}
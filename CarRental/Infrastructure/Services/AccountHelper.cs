using CarRental.Infrastructure.Services.Interfaces;

namespace CarRental.Infrastructure.Services;

public class AccountHelper : IAccountHelper
{
    public string EmailAddress { get; set; } = string.Empty;
}
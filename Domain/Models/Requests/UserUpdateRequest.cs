using Domain.Extensions;
using Domain.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Requests;

public class UserUpdateRequest(string? username, string? email, decimal? balance, string? password)
{
    [MaxLength(25)]
    [UsernameValidator]
    public string? Username { get; } = username.TrimIfNotNull().ConvertEmptyStringToNull();

    [EmailValidator]
    public string? Email { get; } = email.TrimIfNotNull().ConvertEmptyStringToNull();

    public decimal? Balance { get; } = balance;

    [MaxLength(25)]
    public string? Password { get; } = password.ConvertEmptyStringToNull();

    public string? Salt { get; }
}

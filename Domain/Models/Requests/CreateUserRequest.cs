using System.ComponentModel.DataAnnotations;
using Domain.Extensions;
using Domain.Models.Validators;

namespace Domain.Models.Requests;

public class CreateUserRequest(string username, string email, string password)
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(25)]
    [UsernameValidator]
    public string Username { get; } = username.TrimIfNotNull();
    
    [Required(AllowEmptyStrings = false)]
    [EmailValidator]
    public string Email { get; } = email.TrimIfNotNull();

    [Required(AllowEmptyStrings = false)]
    [MaxLength(25)]
    public string Password { get; } = password;
}

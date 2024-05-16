using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Requests;

public class LoginUserRequest(string usernameOrEmail, string password)
{
    [Required(AllowEmptyStrings = false)]
    public string UsernameOrEmail { get; } = usernameOrEmail;

    [Required(AllowEmptyStrings = false)] 
    public string Password { get; } = password;
}


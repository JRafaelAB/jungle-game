using System.ComponentModel.DataAnnotations;
using Domain.Resources;
using Domain.Utils;

namespace Domain.Models.Validators;

public class UsernameValidator : ValidationAttribute 
{
    public UsernameValidator()
    {
        this.ErrorMessage = Messages.InvalidUsername;
    }
    public override bool IsValid(object? value)
    {
        return value != null && value.ToString()!.IsValidUsername();
    }
}

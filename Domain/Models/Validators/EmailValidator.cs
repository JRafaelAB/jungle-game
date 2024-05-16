using System.ComponentModel.DataAnnotations;
using Domain.Resources;
using Domain.Utils;

namespace Domain.Models.Validators;

public class EmailValidator : ValidationAttribute 
{
    public EmailValidator()
    {
        this.ErrorMessage = Messages.InvalidEmail;
    }
    public override bool IsValid(object? value)
    {
        return value != null && value.ToString()!.IsValidEmail();
    }
}

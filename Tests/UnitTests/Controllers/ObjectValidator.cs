using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace UnitTests.Controllers;

public class ObjectValidator : IObjectModelValidator
{

    public void Validate(ActionContext actionContext, ValidationStateDictionary? validationState, string prefix, object? model)
    {
        var context = new ValidationContext(model!, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();

        bool isValid = model != null && Validator.TryValidateObject(
            model, context, results,
            validateAllProperties: true
        );

        if (!isValid)
            results.ForEach((r) =>
            {
                // Add validation errors to the ModelState
                if (r.ErrorMessage != null) actionContext.ModelState.AddModelError("", r.ErrorMessage);
            });
    }
}

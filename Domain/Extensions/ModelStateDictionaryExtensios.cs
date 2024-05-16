namespace Domain.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;


public static class ModelStateDictionaryExtensions
{
    public static IEnumerable<string> GetErrorList(this ModelStateDictionary modelState)
    {
        return modelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
    }
}

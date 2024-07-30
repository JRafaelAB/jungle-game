using System.Text.RegularExpressions;
using Domain.Constants;
using Domain.Resources;

namespace Domain.Utils;

public static class Validation
{
    public static T GetValue<T>(this T? obj)
    {
        if (obj == null)
        {
            throw new ArgumentException(null);
        }

        return obj;
    }

    public static void ValidateNullArgument(this object? obj, string paramName)
    {
        if (obj == null)
        {
            throw new ArgumentException(null, paramName);
        }
    }

    public static void ValidateStringArgumentNotNullOrEmpty(this string? argument, string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new ArgumentException(Messages.ArgumentStringNullOrEmpty, paramName);
        }
    }

    public static bool IsValidEmail(this string argument)
    {
        return Regex.IsMatch(argument, RegexConstants.EMAIL_REGEX, RegexOptions.IgnoreCase);
    }

    public static bool IsValidUsername(this string argument)
    {
        return Regex.IsMatch(argument, RegexConstants.USERNAME_REGEX);
    }
    public static bool HasAllFieldsNull(this object myObject)
    {
        return myObject.GetType().GetProperties()
            .Select(pi => pi.GetValue(myObject))
            .All(value => value == null);
    }
}

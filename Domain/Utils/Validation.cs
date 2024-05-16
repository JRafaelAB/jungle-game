using System.Text.RegularExpressions;
using Domain.Constants;
using Domain.Resources;

namespace Domain.Utils;

public static class Validation
{
    
    
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
}

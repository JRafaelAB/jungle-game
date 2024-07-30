namespace Domain.Extensions;

public static class StringExtensions
{
    public static string TrimIfNotNull(this string? argument)
    {
        return string.IsNullOrWhiteSpace(argument) ? string.Empty : argument.TrimEnd().TrimStart();
    }
    public static string? ConvertEmptyStringToNull(this string? argument)
    {
        return argument.IsNullEmptyOrWhiteSpace() ? null : argument;
    }
    public static bool IsNullEmptyOrWhiteSpace(this string? argument)
    {
        return string.IsNullOrEmpty(argument) || string.IsNullOrWhiteSpace(argument);
    }
}

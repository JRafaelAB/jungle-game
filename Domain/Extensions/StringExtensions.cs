namespace Domain.Extensions;

public static class StringExtensions
{
    public static string TrimIfNotNull(this string argument)
    {
        return string.IsNullOrWhiteSpace(argument) ? string.Empty : argument.TrimEnd().TrimStart();
    }
}

using System.Text;

namespace Domain.Extensions;

public static class StringExtensions
{
    public static string TrimIfNotNull(this string argument)
    {
        return string.IsNullOrWhiteSpace(argument) ? string.Empty : argument.TrimEnd().TrimStart();
    }
    
    public static string GetOnlyNumbers(this string argument)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in argument) {
            if (c is >= '0' and <= '9'){
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}

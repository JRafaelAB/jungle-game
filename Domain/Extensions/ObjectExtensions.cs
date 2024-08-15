namespace Domain.Extensions;

public static class ObjectExtensions
{
    public static string ToStringOrEmpty(this object? obj)
    {
        return obj == null ? string.Empty : obj.ToString()??string.Empty;
    }
}

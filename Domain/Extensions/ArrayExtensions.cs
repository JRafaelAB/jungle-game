namespace Domain.Extensions;

public static class ArrayExtensions
{
    public static bool EqualsTo<T>(this T[] array, T[] other)
    {
        if (array.Length != other.Length) return false;
        return !array.Where((t, i) => !Equals(t, other[i])).Any();
    }
}
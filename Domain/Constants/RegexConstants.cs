namespace Domain.Constants;

public static class RegexConstants
{
    public const string EmailRegex
        = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
    public const string UsernameRegex = @"^((?![@""'\\\/;:\s|()\[\]{}]).)*$";
}

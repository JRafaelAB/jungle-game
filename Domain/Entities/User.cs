using Domain.DTOs;

namespace Domain.Entities;

public class User(string username, string email, string password, string salt, ulong? Id = null)
{
    public ulong? Id { get; } = Id;
    public string Username { get; } = username;
    public string Email { get; } = email;
    public string Password { get; } = password;
    public string Salt { get; } = salt;

    public User(UserDto userDto) : this(userDto.Username, userDto.Email, userDto.Password, userDto.Salt, userDto.Id) { }

    protected bool Equals(User other)
    {
        return Username == other.Username && Email == other.Email && Password == other.Password && Salt == other.Salt;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((User)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Username, Email, Password, Salt);
    }

    public static bool operator ==(User? left, User? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(User? left, User? right)
    {
        return !Equals(left, right);
    }
}
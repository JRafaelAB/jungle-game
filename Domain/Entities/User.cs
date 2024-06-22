using Domain.DTOs;

namespace Domain.Entities;

public class User(string username, string email, string password, string salt, ulong? Id = null)
{
    public ulong? Id { get; } = Id;
    public string Username { get; private set; } = username;
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;
    public string Salt { get; private set; } = salt;

    public User(UserDto userDto) : this(userDto.Username, userDto.Email, userDto.Password, userDto.Salt, userDto.Id) { }

    public void Update(UserDto userDto)
    {
        this.Username = userDto.Username;
        this.Email = userDto.Email;
        this.Password = userDto.Password;
        this.Salt = userDto.Salt ?? this.Salt;
    }
    
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
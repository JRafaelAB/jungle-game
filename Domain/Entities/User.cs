using Domain.DTOs;
using Domain.Utils;

namespace Domain.Entities;

public class User(string username, string email, string password, decimal balance, string salt, ulong? id = null)
{
    public ulong? Id { get; } = id;
    public string Username { get; private set; } = username;
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;
    public decimal Balance { get; private set; } = balance;
    public string Salt { get; private set; } = salt;

    public User(UserDto userDto) : this(userDto.Username.GetValue(), userDto.Email.GetValue(), userDto.Password.GetValue(), userDto.Balance ?? 0, userDto.Salt.GetValue(), userDto.Id) { }

    public void Update(UserDto userDto)
    {
        this.Username = userDto.Username ?? this.Username;
        this.Email = userDto.Email ?? this.Email;
        this.Password = userDto.Password ?? this.Password;
        this.Balance = userDto.Balance ?? this.Balance;
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
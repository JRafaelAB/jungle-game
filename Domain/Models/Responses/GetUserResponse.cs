using Domain.DTOs;

namespace Domain.Models.Responses;

public class GetUserResponse(UserDto user)
{
    public ulong? Id { get; } = user.Id;
    public string Username { get; } = user.Username;
    

    protected bool Equals(GetUserResponse other)
    {
        return this.Username == other.Username && this.Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((GetUserResponse)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Username, this.Id);
    }

    public static bool operator ==(GetUserResponse? left, GetUserResponse? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GetUserResponse? left, GetUserResponse? right)
    {
        return !Equals(left, right);
    }
}

using Domain.DTOs;

namespace Domain.Models.Responses;

public class GetUserResponse(UserDto user)
{
    public ulong? Id { get; } = user.Id;
    public string Username { get; } = user.Username;
}

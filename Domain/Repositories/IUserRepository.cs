using Domain.DTOs;

namespace Domain.Repositories;

public interface IUserRepository
{
    public Task<ulong?> AddUser(UserDto userDto);
    public Task<UserDto?> GetUser(ulong userId);
    public Task<UserDto?> GetUserByUsername(string username);
    public Task<UserDto?> GetUserByEmail(string email);
    public Task<UserDto?> GetUserByUsernameOrEmail(string usernameOrEmail);
}

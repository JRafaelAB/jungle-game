using Domain.DTOs;

namespace Domain.Repositories;

public interface IUserRepository
{
    public Task AddUser(UserDto userDto);
    public Task<UserDto?> GetUserByUsername(string username);
    public Task<UserDto?> GetUserByEmail(string email);
    public Task<UserDto?> GetUserByUsernameOrEmail(string usernameOrEmail);
    public Task<bool> UpdateUser(UserDto userDto, string user);
}
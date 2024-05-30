using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(JungleContext context) : IUserRepository
{
    public async Task<ulong?> AddUser(UserDto userDto)
    {
        User userEntity = new(userDto);
        return (await context.Users.AddAsync(userEntity)).Entity.Id;
    } 
    
    public async Task<UserDto?> GetUser(ulong userId)
    {
        var user = await context.Users.Where(user => user.Id == userId)
            .SingleOrDefaultAsync();
        return user == null ? null : new UserDto(user);
    }

    public async Task<UserDto?> GetUserByUsername(string username)
    {
        var user = await context.Users.Where(user => user.Username == username)
            .SingleOrDefaultAsync();
        return user == null ? null : new UserDto(user);
    }

    public async Task<UserDto?> GetUserByEmail(string email)
    {
        var user = await context.Users.Where(user => user.Email == email)
            .SingleOrDefaultAsync();
        return user == null ? null : new UserDto(user);
    }

    public async Task<UserDto?> GetUserByUsernameOrEmail(string usernameOrEmail)
    {
        var user = await context.Users.Where(user => user.Email == usernameOrEmail || user.Username == usernameOrEmail)
            .SingleOrDefaultAsync();
        return user == null ? null : new UserDto(user);
    }
}
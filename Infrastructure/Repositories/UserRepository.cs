﻿using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(JungleContext context) : IUserRepository
{
    public async Task AddUser(UserDto userDto)
    {
        User userEntity = new(userDto);
        await context.Users.AddAsync(userEntity);
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
    public async Task<bool> UpdateUser(UserDto userDto, string user)
    {
        var userEntity = await context.Users.Where(userEntity => userEntity.Email == user || userEntity.Username == user)
            .SingleOrDefaultAsync();
        if (userEntity == null) return false;
        userEntity.Update(userDto);
        return true;
    }
    public async Task<bool> DeleteUser(string user)
    {
        var userEntity = await context.Users.Where(userEntity => userEntity.Email == user || userEntity.Username == user)
            .SingleOrDefaultAsync();
        if (userEntity == null) return false;
        context.Users.Remove(userEntity);
        return true;
    }
}
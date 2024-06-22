using Domain.DTOs;
using Domain.Entities;
using Domain.Models.Requests;
using Domain.Utils;
using Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

public class JungleContextMock(string databaseName)
{
    public async Task<JungleContext> StartNewContext()
    {
        var options = new DbContextOptionsBuilder<JungleContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;
        
        var context = new JungleContext(options);
        await context.Database.EnsureDeletedAsync();
        await context.Users.AddAsync(User1);
        await context.Users.AddAsync(User2);
        await context.Users.AddAsync(User3);
        await context.Users.AddAsync(User4);
        await context.Users.AddAsync(User5);
        await context.Users.AddAsync(User6);
        await context.Users.AddAsync(User7);
        await context.Users.AddAsync(User8);
        await context.Users.AddAsync(User9);
        await context.SaveChangesAsync();
        return context;
    }

    public static readonly User User1 = new("user1", "user1@gmail.com", Cryptography.EncryptPassword("password", "salt"), "salt");
    public static readonly UserDto User1Dto = new("user1", "user1@gmail.com", Cryptography.EncryptPassword("password", 
        "salt"), "salt");
    public static readonly UserDto User2Dto = new("user2", "user2@gmail.com", Cryptography.EncryptPassword("password",
        "salt"), "salt");
    public static readonly User User2 = new("user2", "user2@gmail.com", Cryptography.EncryptPassword("password", "salt"), "salt");
    public static readonly User User3 = new("user3", "user3@gmail.com", Cryptography.EncryptPassword("password", "salt"), "salt");
    public static readonly User User4 = new("user4", "user4@gmail.com", Cryptography.EncryptPassword("password", "salt"), "salt");
    public static readonly User User5 = new("user5", "user5@gmail.com", Cryptography.EncryptPassword("password", "salt"), "salt");
    public static readonly User User6 = new("user6", "user6@gmail.com", Cryptography.EncryptPassword("password", "salt"), "salt");
    public static readonly User User7 = new("user7", "user7@gmail.com", Cryptography.EncryptPassword("password", "salt"), "salt");
    public static readonly User User8 = new("user8", "user8@gmail.com", Cryptography.EncryptPassword("password", "salt"), "salt");
    public static readonly User User9 = new("user9", "user9@gmail.com", Cryptography.EncryptPassword("password", "salt"), "salt");
    public static readonly UserDto NewUser1Dto = new("NewUser1", "NewUser1@gmail.com", "password", "salt");
    public static readonly User NewUser1 = new("NewUser1", "NewUser1@gmail.com", "password", "salt");
    public static readonly UserRequest User1Request = new("user1", "user1@gmail.com", Cryptography.EncryptPassword("password", "salt"));
    public static readonly UserRequest UpdateUser11Request = new("user11", "user11@gmail.com", Cryptography.EncryptPassword("password", "salt"));
    public static readonly UserRequest UpdateUserExistingEmailRequest = new("user1", "user2@gmail.com", Cryptography.EncryptPassword("password", "salt"));
    public static readonly UserRequest UpdateUserExistingUsernameRequest = new("user2", "user1@gmail.com", Cryptography.EncryptPassword("password", "salt"));

    public static readonly LotteryDTO Lottery1Dto = new([1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0], 4);
    public static readonly LotteryResults Lottery1 = new(4,"1-2,3-4", "5-6,7-8", "9-0,1-2", "3-4,5-6", "7-8,9-0", DateTime.Now)
    {
        Id = 1
    };
}

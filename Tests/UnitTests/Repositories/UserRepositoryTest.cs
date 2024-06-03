using Domain.DTOs;
using Domain.Models.Requests;
using Domain.Utils;
using Infrastructure.Repositories;
using Xunit;

namespace UnitTests.Repositories;

public class UserRepositoryTest
{
    public UserRepositoryTest()
    {
        Configuration.SetConfiguration(TestConfigurationBuilder.BuildTestConfiguration());
    }

    [Fact]
    public async Task Test_Add_New_User()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        await repository.AddUser(JungleContextMock.NewUser1Dto);
        await context.SaveChangesAsync();
        Assert.Contains(JungleContextMock.NewUser1, context.Users);
    }
    
    [Fact]
    public async Task Test_Get_Existing_User_By_Username()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsername(JungleContextMock.User1.Username);
        Assert.Equal(new UserDto(JungleContextMock.User1), result);
    }
    
    [Fact]
    public async Task Test_Get_Non_Existing_Username()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsername("Bananinha Maligna");
        Assert.Null(result);
    }
    
    [Fact]
    public async Task Test_Get_Existing_User_By_Email()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByEmail(JungleContextMock.User1.Email);
        Assert.Equal(new UserDto(JungleContextMock.User1), result);
    }
    
    [Fact]
    public async Task Test_Get_Non_Existing_Email()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByEmail("Bananinha Maligna");
        Assert.Null(result);
    }
    
    [Fact]
    public async Task Test_Get_Existing_User_By_UsernameOrEmail_Using_Username()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsernameOrEmail(JungleContextMock.User1.Username);
        Assert.Equal(new UserDto(JungleContextMock.User1), result);
    }
    
    [Fact]
    public async Task Test_Get_Existing_User_By_UsernameOrEmail_Using_Email()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsernameOrEmail(JungleContextMock.User1.Email);
        Assert.Equal(new UserDto(JungleContextMock.User1), result);
    }
    
    [Fact]
    public async Task Test_Get_Non_Existing_User_By_UsernameOrEmail()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsernameOrEmail("Bananinha Maligna");
        Assert.Null(result);
    }
<<<<<<< HEAD
    
    [Fact]
    public async Task Test_Get_Existing_User()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUser(4);
        Assert.NotNull(result);
        Assert.Equal(new UserDto(JungleContextMock.User4), result);
    }
    
    [Fact]
    public async Task Test_Get_Non_Existing_User()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUser(10);
        Assert.Null(result);
    }

    [Fact]
    public async Task Test_Update_Non_Existing_User()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var request = new UserRequest("Pedro", "pedro@teste.com", "1234");
        var userDto = new UserDto(request);
        var result = await repository.UpdateUser(userDto, "Jonas");
        Assert.False(result);
    }

    [Fact]
    public async Task Test_Update_Existing_User_By_Username()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var request = new UserRequest("Pedro", "pedro@teste.com", "1234");
        var userDto = new UserDto(request);
        var result = await repository.UpdateUser(userDto, "user8");
        Assert.True(result);
        var user = await context.Users.FindAsync((ulong)8);
        Assert.Equal("Pedro", user!.Username);
        Assert.Equal("pedro@teste.com", user.Email);
        var password = Cryptography.EncryptPassword("1234", user.Salt);
        Assert.Equal(password, user.Password);
    }

    [Fact]
    public async Task Test_Update_Existing_User_By_Email()
    {
        await using var context = JungleContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var request = new UserRequest("Pedro", "pedro@teste.com", "1234");
        var userDto = new UserDto(request);
        var salt = userDto.Salt;
        var result = await repository.UpdateUser(userDto, "user9@gmail.com");
        Assert.True(result);
        var user = await context.Users.FindAsync((ulong)9);
        Assert.Equal("Pedro", user!.Username);
        Assert.Equal("pedro@teste.com", user.Email);
        var password = Cryptography.EncryptPassword("1234", user.Salt);
        Assert.Equal(password, user.Password);
    }
}
=======
}
>>>>>>> main

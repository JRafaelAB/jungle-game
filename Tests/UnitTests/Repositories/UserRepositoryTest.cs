using Domain.DTOs;
using Infrastructure.Repositories;
using Xunit;

namespace UnitTests.Repositories;

public class UserRepositoryTest
{
    
    [Fact]
    public async Task Test_Add_New_User()
    {
        await using var context = ToolsContextMock.StartNewContext();
        var repository = new UserRepository(context);
        await repository.AddUser(ToolsContextMock.NewUser1Dto);
        await context.SaveChangesAsync();
        Assert.Contains(ToolsContextMock.NewUser1, context.Users);
    }
    
    [Fact]
    public async Task Test_Get_Existing_User_By_Username()
    {
        await using var context = ToolsContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsername(ToolsContextMock.User1.Username);
        Assert.Equal(new UserDto(ToolsContextMock.User1), result);
    }
    
    [Fact]
    public async Task Test_Get_Non_Existing_Username()
    {
        await using var context = ToolsContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsername("Bananinha Maligna");
        Assert.Null(result);
    }
    
    [Fact]
    public async Task Test_Get_Existing_User_By_Email()
    {
        await using var context = ToolsContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByEmail(ToolsContextMock.User1.Email);
        Assert.Equal(new UserDto(ToolsContextMock.User1), result);
    }
    
    [Fact]
    public async Task Test_Get_Non_Existing_Email()
    {
        await using var context = ToolsContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByEmail("Bananinha Maligna");
        Assert.Null(result);
    }
    
    [Fact]
    public async Task Test_Get_Existing_User_By_UsernameOrEmail_Using_Username()
    {
        await using var context = ToolsContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsernameOrEmail(ToolsContextMock.User1.Username);
        Assert.Equal(new UserDto(ToolsContextMock.User1), result);
    }
    
    [Fact]
    public async Task Test_Get_Existing_User_By_UsernameOrEmail_Using_Email()
    {
        await using var context = ToolsContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsernameOrEmail(ToolsContextMock.User1.Email);
        Assert.Equal(new UserDto(ToolsContextMock.User1), result);
    }
    
    [Fact]
    public async Task Test_Get_Non_Existing_User_By_UsernameOrEmail()
    {
        await using var context = ToolsContextMock.StartNewContext();
        var repository = new UserRepository(context);
        var result = await repository.GetUserByUsernameOrEmail("Bananinha Maligna");
        Assert.Null(result);
    }
}

using Application.UseCases.LoginUser;
using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Repositories;
using Domain.Resources;
using Domain.Utils;
using Moq;
using Xunit;

namespace UnitTests.UseCases.LoginUser;

public class LoginUserUseCaseTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly LoginUserUseCase _useCase;

    public LoginUserUseCaseTest()
    {
        Configuration.SetConfiguration(TestConfigurationBuilder.BuildTestConfiguration());
        this._userRepository = new Mock<IUserRepository>();
        ConfigureUserRepositoryForExistingLogin();
        this._useCase = new LoginUserUseCase(_userRepository.Object);
    }

    [Fact]
    public async Task Test_Login_Existing_User_By_Username()
    {
        var result = await this._useCase.Execute(new LoginUserRequest("user1", "password"));
        Assert.Equal((ulong)1, result);
    }

    [Fact]
    public async Task Test_Login_Existing_User_By_Email()
    {
        var result = await this._useCase.Execute(new LoginUserRequest("user1@gmail.com", "password"));
        Assert.Equal((ulong)1, result);
    }

    [Fact]
    public async Task Test_Login_Non_Existing_User_By_Username()
    {
        Exception? exception = null;
        try
        {
            await this._useCase.Execute(new LoginUserRequest("notuser", "password"));
        }
        catch (Exception ex)
        {
            exception = ex;
        }
        Assert.NotNull(exception);
        Assert.IsType<UserNotFoundException>(exception);
        Assert.Equal(Messages.InvalidUser, ((UserNotFoundException)exception).ErrorMessages.SingleOrDefault());
    }

    [Fact]
    public async Task Test_Login_Non_Existing_User_By_Email()
    {
        Exception? exception = null;
        try
        {
            await this._useCase.Execute(new LoginUserRequest("notuser@gmail.com", "password"));
        }
        catch (Exception ex)
        {
            exception = ex;
        }
        Assert.NotNull(exception);
        Assert.IsType<UserNotFoundException>(exception);
        Assert.Equal(Messages.InvalidUser, ((UserNotFoundException)exception).ErrorMessages.SingleOrDefault());
    }

    [Fact]
    public async Task Test_Login_Existing_User_With_Wrong_Password()
    {
        Exception? exception = null;
        try
        {
            await this._useCase.Execute(new LoginUserRequest("user1@gmail.com", "wrongpassword"));
        }
        catch (Exception ex)
        {
            exception = ex;
        }
        Assert.NotNull(exception);
        Assert.IsType<UserNotFoundException>(exception);
        Assert.Equal(Messages.InvalidUser, ((UserNotFoundException)exception).ErrorMessages.SingleOrDefault());
    }

    private void ConfigureUserRepositoryForExistingLogin()
    {
        this._userRepository.Setup(repo => repo.GetUserByUsernameOrEmail("user1")).ReturnsAsync(JungleContextMock.User1Dto);
        
        this._userRepository.Setup(repo => repo.GetUserByUsernameOrEmail("user1@gmail.com")).ReturnsAsync(JungleContextMock.User1Dto);
    }
}

using Application.UseCases.CreateUser;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnitOfWork;
using Domain.Utils;
using Moq;
using Xunit;

namespace UnitTests.UseCases.CreateUser;

public class PostUserUseCaseTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreateUserUseCase _useCase;

    public PostUserUseCaseTest()
    {
        Configuration.SetConfiguration(TestConfigurationBuilder.BuildTestConfiguration());
        this._userRepository = new Mock<IUserRepository>();
        this._unitOfWork = new Mock<IUnitOfWork>();
        this._useCase = new CreateUserUseCase(_userRepository.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task Test_Create_New_User()
    {
        UserDto user = new("usuario", "email", "10", "10");

        await _useCase.Execute(CreateUserUseCaseDataSetup.validUser);
        
        this._userRepository.Verify(repo => repo.AddUser(user), Times.Once);
        this._userRepository.Verify(repo => repo.GetUserByUsername("usuario"), Times.Once);
        this._userRepository.Verify(repo => repo.GetUserByEmail("email"), Times.Once);
        this._unitOfWork.Verify(x => x.Save(), Times.Once);
    }

    [Fact]
    public async Task Test_Create_User_Existing_Username_And_Email()
    {
        ConfigureUserRepositoryForExistingLogin();
        
        UserDto user = new("usuario", "email", "10", "10");
        LoginConflictException? ex = null;
        try
        {
            await _useCase.Execute(CreateUserUseCaseDataSetup.validUser);
        }
        catch (LoginConflictException exception)
        {
            ex = exception;
        }
        Assert.NotNull(ex);
        this._userRepository.Verify(repo => repo.GetUserByUsername("usuario"), Times.Once);
        this._userRepository.Verify(repo => repo.GetUserByEmail("email"), Times.Once);
        this._userRepository.Verify(repo => repo.AddUser(user), Times.Never);
        this._unitOfWork.Verify(x => x.Save(), Times.Never);
        Assert.Equal(2, ex!.ErrorMessages.Count);
        Assert.Contains("Email already registered.", ex!.ErrorMessages);
        Assert.Contains("Username already registered.", ex!.ErrorMessages);
    }

    private void ConfigureUserRepositoryForExistingLogin()
    {
        this._userRepository.Setup(repo => repo.GetUserByUsername("usuario")).ReturnsAsync(new UserDto("usuario", 
            "email", "10", "10"));
        this._userRepository.Setup(repo => repo.GetUserByEmail("email")).ReturnsAsync(new UserDto("usuario", 
            "email", "10", "10"));
    }
}

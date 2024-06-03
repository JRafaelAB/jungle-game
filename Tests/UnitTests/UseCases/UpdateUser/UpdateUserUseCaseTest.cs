using Application.UseCases.UpdateUser;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Resources;
using Domain.UnitOfWork;
using Domain.Utils;
using Moq;
using Xunit;

namespace UnitTests.UseCases.UpdateUser;

public class UpdateUserUseCaseTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly UpdateUserUseCase _useCase;

    public UpdateUserUseCaseTest()
    {
        Configuration.SetConfiguration(TestConfigurationBuilder.BuildTestConfiguration());
        this._userRepository = new Mock<IUserRepository>();
        this._unitOfWork = new Mock<IUnitOfWork>();
        this._useCase = new(this._userRepository.Object, this._unitOfWork.Object);
        ConfigureUserRepositoryForExistingLogin();
    }

    [Fact]
    public async Task Test_UseCase_Update_Non_Existing_Id()
    {
        var ex = await Assert.ThrowsAsync<UserNotFoundException>(() => _useCase.Execute(JungleContextMock.UpdateUser11Request,"user11"));
        Assert.Equal(Messages.UserNotFound, ex.Message);
    }

    [Fact]
    public async Task Test_UseCase_Update_Existing_Id()
    {
        await  _useCase.Execute(JungleContextMock.User1Request,"user1");
        _userRepository.Verify(x => x.UpdateUser(new UserDto(JungleContextMock.User1Request), "user1"), Times.Once);
        _unitOfWork.Verify(x => x.Save(), Times.Once);
    }

    [Fact]
    public async Task Test_UseCase_Update_Existing_Id_With_Existing_Email()
    {
        var ex = await Assert.ThrowsAsync<LoginConflictException>(() => _useCase.Execute(JungleContextMock.UpdateUserExistingEmailRequest, "user1"));
        Assert.Single(ex.ErrorMessages);
        Assert.Equal(Messages.ConflictEmail, ex.ErrorMessages.First());
    }

    [Fact]
    public async Task Test_UseCase_Update_Existing_Id_With_Existing_Username()
    {
        var ex = await Assert.ThrowsAsync<LoginConflictException>(() => _useCase.Execute(JungleContextMock.UpdateUserExistingUsernameRequest, "user1"));
        Assert.Single(ex.ErrorMessages);
        Assert.Equal(Messages.ConflictUsername, ex.ErrorMessages.First());
    }

    private void ConfigureUserRepositoryForExistingLogin()
    {
        this._userRepository.Setup(repo => repo.UpdateUser(It.IsAny<UserDto>(), "user1")).
            ReturnsAsync(true);

        this._userRepository.Setup(repo => repo.UpdateUser(new UserDto(JungleContextMock.User1Request), "user11")).
            ReturnsAsync(false);

        this._userRepository.Setup(repo => repo.GetUserByUsername("user1")).
            ReturnsAsync(JungleContextMock.User1Dto);

        this._userRepository.Setup(repo => repo.GetUserByEmail("user1@gmail.com")).
            ReturnsAsync(JungleContextMock.User1Dto);

        this._userRepository.Setup(repo => repo.GetUserByUsername("user2")).
            ReturnsAsync(JungleContextMock.User2Dto);

        this._userRepository.Setup(repo => repo.GetUserByEmail("user2@gmail.com")).
            ReturnsAsync(JungleContextMock.User2Dto);
    }

}
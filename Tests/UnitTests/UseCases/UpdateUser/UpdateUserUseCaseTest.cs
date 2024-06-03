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
        var ex = await Assert.ThrowsAsync<UserNotFoundException>(() => _useCase.Execute(JungleContextMock.User1Request,"user11"));
        Assert.Equal(Messages.UserNotFound, ex.Message);
    }

    [Fact]
    public async Task Test_UseCase_Update_Existing_Id()
    {
        await  _useCase.Execute(JungleContextMock.User1Request,"user1");
        _userRepository.Verify(x => x.UpdateUser(new UserDto(JungleContextMock.User1Request), "user1"), Times.Once);
        _unitOfWork.Verify(x => x.Save(), Times.Once);
    }

    private void ConfigureUserRepositoryForExistingLogin()
    {
        this._userRepository.Setup(repo => repo.UpdateUser(new UserDto(JungleContextMock.User1Request), "user1")).
            ReturnsAsync(true);

        this._userRepository.Setup(repo => repo.UpdateUser(new UserDto(JungleContextMock.User1Request), "user11")).
            ReturnsAsync(false);
    }

}
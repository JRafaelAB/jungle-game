using Application.UseCases.DeleteUser;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Resources;
using Domain.UnitOfWork;
using Domain.Utils;
using Moq;
using Xunit;

namespace UnitTests.UseCases.DeleteUser;

public class DeleteUserUseCaseTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly DeleteUserUseCase _useCase;

    public DeleteUserUseCaseTest()
    {
        Configuration.SetConfiguration(TestConfigurationBuilder.BuildTestConfiguration());
        this._userRepository = new Mock<IUserRepository>();
        this._unitOfWork = new Mock<IUnitOfWork>();
        this._useCase = new(this._userRepository.Object, this._unitOfWork.Object);
        ConfigureUserRepositoryForExistingLogin();
    }

    [Fact]
    public async Task Test_UseCase_Update_Existing_User()
    {
        await _useCase.Execute("user1");
        _userRepository.Verify(x => x.DeleteUser("user1"), Times.Once);
        _unitOfWork.Verify(x => x.Save(), Times.Once);
    }

    [Fact]
    public async Task Test_UseCase_Delete_Non_Existing_User()
    {
        var ex = await Assert.ThrowsAsync<UserNotFoundException>(() => _useCase.Execute("user11"));
        Assert.Equal(Messages.UserNotFound, ex.ErrorMessages.First());
    }
    private void ConfigureUserRepositoryForExistingLogin()
    {
        this._userRepository.Setup(repo => repo.DeleteUser("user1")).
            ReturnsAsync(true);
    }
}

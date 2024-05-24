using Application.UseCases.GetUser;
using Domain.Exceptions;
using Domain.Models.Responses;
using Domain.Repositories;
using Domain.Resources;
using Moq;
using Xunit;

namespace UnitTests.UseCases.GetUser;

public class GetUserUseCaseTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly IGetUserUseCase _useCase;
    
    public GetUserUseCaseTest()
    {
        _userRepository = new Mock<IUserRepository>();
        ConfigureUserRepositoryForExistingUser();
        _useCase = new GetUserUseCase(_userRepository.Object);
    }
    
    

    [Fact]
    public async Task Test_Get_Existing_User()
    {
        var result = await this._useCase.Execute(1);
        Assert.Equal(new GetUserResponse(JungleContextMock.User1Dto), result);
    }

    [Fact]
    public async Task Test_Get_Non_Existing_User()
    {
        var ex = await Assert.ThrowsAsync<UserNotFoundException>(() => this._useCase.Execute(10));
        Assert.Equal(Messages.UserNotFound, ex.Message);
    }
    
    private void ConfigureUserRepositoryForExistingUser()
    {
        this._userRepository.Setup(repo => repo.GetUser(1)).ReturnsAsync(JungleContextMock.User1Dto);
    }
}

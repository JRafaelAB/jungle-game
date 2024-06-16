using Application.UseCases.DeleteUser;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers.DeleteUser;
using Xunit;

namespace UnitTests.Controllers.DeleteUser;

public class UsersControllerTest
{
    private readonly UsersController _controller;
    private readonly Mock<IDeleteUserUseCase> _loginUser = new();

    public UsersControllerTest()
    {
        this._controller = new UsersController(_loginUser.Object);
    }

    [Fact]
    public async Task Test_Delete_Existing_User()
    {
        var result = await this._controller.DeleteUser("ExistingUser");
        Assert.IsType<NoContentResult>(result);
    }
}

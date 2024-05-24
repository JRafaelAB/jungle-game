using Application.UseCases.GetUser;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers.GetUser;
using Xunit;

namespace UnitTests.Controllers.GetUser;

public class UsersControllerTest
{
    private readonly UsersController _controller;
    private readonly Mock<IGetUserUseCase> _loginUser = new ();

    public UsersControllerTest()
    {
        this._controller = new UsersController(_loginUser.Object);
    }

    [Fact]
    public async Task Test_Get_Existing_User()
    {
        var result = await this._controller.GetUser(1);
        Assert.IsType<OkObjectResult>(result);
    }
    
}

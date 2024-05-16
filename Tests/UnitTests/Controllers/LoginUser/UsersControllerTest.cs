using Application.UseCases.LoginUser;
using Domain.Exceptions;
using Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using WebApi.Controllers.LoginUser;
using Xunit;

namespace UnitTests.Controllers.LoginUser;

public class UsersControllerTest
{
    private readonly UsersController _controller;
    private readonly Mock<ILoginUserUseCase> _loginUser;

    public UsersControllerTest()
    {
        this._loginUser = new Mock<ILoginUserUseCase>();
        this._controller = new UsersController(_loginUser.Object);
    }

    [Fact]
    public async Task Test_LoginUser_Valid_Request()
    {
        ConfigureObjectValidator();
        var successRequest = JsonConvert.DeserializeObject<LoginUserRequest>("{\"UsernameOrEmail\":\"name\",\"Password\":\"password\"}");
        var result = await this._controller.LoginUser(successRequest!);
        Assert.IsType<OkObjectResult>(result);
        this._loginUser.Verify(x => x.Execute(successRequest!), Times.Once);
    }

    [Fact]
    public async Task Test_PostUser_Invalid_Request_Empty_Fields()
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<LoginUserRequest>("{\"UsernameOrEmail\":\" \",\"Password\":\" \"}");
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.LoginUser(request!);
        }
        catch (InvalidRequestException exception)
        {
            ex = exception;
        }

        Assert.NotNull(ex);
        var errors = ex!.ErrorMessages;
        Assert.Equal(2, errors.Count);
        Assert.Contains("The UsernameOrEmail field is required.", errors);
        Assert.Contains("The Password field is required.", errors);
    }

    [Fact]
    public async Task Test_PostUser_Invalid_Request_Missing_Fields()
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<LoginUserRequest>("{}");
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.LoginUser(request!);
        }
        catch (InvalidRequestException exception)
        {
            ex = exception;
        }

        Assert.NotNull(ex);
        var errors = ex!.ErrorMessages;
        Assert.Equal(2, errors.Count);
        Assert.Contains("The UsernameOrEmail field is required.", errors);
        Assert.Contains("The Password field is required.", errors);
    }
    
    private void ConfigureObjectValidator()
    {
        this._controller.ObjectValidator = new ObjectValidator();
    }
}

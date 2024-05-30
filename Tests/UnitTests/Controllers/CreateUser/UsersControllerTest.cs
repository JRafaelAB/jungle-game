using Application.UseCases.CreateUser;
using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using WebApi.Controllers.CreateUser;
using Xunit;

namespace UnitTests.Controllers.CreateUser;

public class UsersControllerTest
{
    private readonly UsersController _controller;
    private readonly Mock<ICreateUserUseCase> _createUser;

    public UsersControllerTest()
    {
        this._createUser = new Mock<ICreateUserUseCase>();
        this._controller = new UsersController(_createUser.Object);
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                Scheme = "http",
                Host = new HostString("localhost")
            }
        };
        var controllerContext = new ControllerContext() {
            HttpContext = httpContext
        };
        _controller.ControllerContext = controllerContext;
    }

    [Fact]
    public async Task Test_PostUser_Valid_Request()
    {
        ConfigureObjectValidator();
        var successRequest = JsonConvert.DeserializeObject<CreateUserRequest>("{\"Username\":\"name\",\"Email\":\"email@email.com\",\"Password\":\"password\"}");
        var result = await this._controller.CreateUser(successRequest!);
        Assert.IsType<CreatedResult>(result);
        this._createUser.Verify(x => x.Execute(successRequest!), Times.Once);
    }

    [Fact]
    public async Task Test_PostUser_Invalid_Request_Empty_Fields()
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<CreateUserRequest>("{\"Username\":\" \",\"Email\":\" \",\"Password\":\" \"}");
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.CreateUser(request!);
        }
        catch (InvalidRequestException exception)
        {
            ex = exception;
        }
        
        Assert.NotNull(ex);
        var errors = ex!.ErrorMessages;
        Assert.Equal(3, errors.Count);
        Assert.Contains("The Username field is required.", errors);
        Assert.Contains("The Password field is required.", errors);
        Assert.Contains("The Email field is required.", errors);
    }

    [Fact]
    public async Task Test_PostUser_Invalid_Request_Fields_Out_Of_Max_Range()
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<CreateUserRequest>("{\"Username\":\"becauseofyouIneverhsjhdjdshjsdds\",\"Email\":\"email@email.com\",\"Password\":\"becauseofyouIneverhsjhdjdshjsdds\"}");
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.CreateUser(request!);
        }
        catch (InvalidRequestException exception)
        {
            ex = exception;
        }
        
        Assert.NotNull(ex);
        var errors = ex!.ErrorMessages;
        Assert.Equal(2, errors.Count);
        Assert.Contains("The field Username must be a string or array type with a maximum length of '25'.", errors);
        Assert.Contains("The field Password must be a string or array type with a maximum length of '25'.", errors);
    }

    [Fact]
    public async Task Test_PostUser_Invalid_Request_Missing_Fields()
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<CreateUserRequest>("{}");
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.CreateUser(request!);
        }
        catch (InvalidRequestException exception)
        {
            ex = exception;
        }
        
        Assert.NotNull(ex);
        var errors = ex!.ErrorMessages;
        Assert.Equal(3, errors.Count);
        Assert.Contains("The Username field is required.", errors);
        Assert.Contains("The Password field is required.", errors);
        Assert.Contains("The Email field is required.", errors);
    }

    [Theory]
    [InlineData("{\"Username\":\"username\",\"Email\":\"email@email\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"username\",\"Email\":\"email\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"username\",\"Email\":\"email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"username\",\"Email\":\"email@\",\"Password\":\"Psswrd\"}")]
    public async Task Test_PostUser_Invalid_Request_Invalid_Email_Format(string? requestModel)
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<CreateUserRequest>(requestModel!);
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.CreateUser(request!);
        }
        catch (InvalidRequestException exception)
        {
            ex = exception;
        }
        
        Assert.NotNull(ex);
        var errors = ex!.ErrorMessages;
        Assert.Equal(1, errors.Count);
        Assert.Contains(Messages.InvalidEmail, errors);
    }

    [Theory]
    [InlineData("{\"Username\":\"Username Longe\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"Username@\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"Username'\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"Username\\\"\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"Username/\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"Username\\\\\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"Username;\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"Username:\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"User[name\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"User]name\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"User{name\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"User}name\",\"Email\":\"email@email.com\",\"Password\":\"Psswrd\"}")]
    public async Task Test_PostUser_Invalid_Request_Invalid_Username_Format(string? requestModel)
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<CreateUserRequest>(requestModel!);
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.CreateUser(request!);
        }
        catch (InvalidRequestException exception)
        {
            ex = exception;
        }
        
        Assert.NotNull(ex);
        var errors = ex!.ErrorMessages;
        Assert.Equal(1, errors.Count);
        Assert.Contains(Messages.InvalidUsername, errors);
    }

    private void ConfigureObjectValidator()
    {
        this._controller.ObjectValidator = new ObjectValidator();
    }
}

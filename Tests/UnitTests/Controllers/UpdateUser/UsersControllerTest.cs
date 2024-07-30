using Application.UseCases.UpdateUser;
using Domain.Exceptions;
using Domain.Models.Requests;
using Domain.Resources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using WebApi.Controllers.UpdateUser;
using Xunit;

namespace UnitTests.Controllers.UpdateUser;

public class UsersControllerTest
{
    private readonly UsersController _controller;
    private readonly Mock<IUpdateUserUseCase> _updateUser;

    public UsersControllerTest()
    {
        this._updateUser = new Mock<IUpdateUserUseCase>();
        this._controller = new UsersController(_updateUser.Object);
    }

    [Fact]
    public async Task Test_PostUser_Valid_Request()
    {
        ConfigureObjectValidator();
        var successRequest = JsonConvert.DeserializeObject<UserUpdateRequest>("{\"Username\":\"name\",\"Email\":\"email@email.com\",\"Password\":\"password\",\"Balance\":\"10\"}");
        var result = await this._controller.UpdateUser("Pedro",successRequest!);
        Assert.IsType<NoContentResult>(result);
        this._updateUser.Verify(x => x.Execute(successRequest!, "Pedro"), Times.Once);
    }

    [Fact]
    public async Task Test_PostUser_Invalid_Request_Empty_Fields()
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<UserUpdateRequest>("{\"Username\":\" \",\"Email\":\" \",\"Password\":\" \",\"Balance\":\"\"}");
        

        var result = await this._controller.UpdateUser("Pedro", request!);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Test_PostUser_Invalid_Request_Fields_Out_Of_Max_Range()
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<UserUpdateRequest>("{\"Username\":\"becauseofyouIneverhsjhdjdshjsdds\",\"Email\":\"email@email.com\",\"Password\":\"becauseofyouIneverhsjhdjdshjsdds\",\"Balance\": \"10\"}");
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.UpdateUser("Pedro", request!);
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
        var request = JsonConvert.DeserializeObject<UserUpdateRequest>("{}");
        
        var result = await this._controller.UpdateUser("Pedro", request!);


        Assert.IsType<BadRequestResult>(result);
    }

    [Theory]
    [InlineData("{\"Username\":\"username\",\"Email\":\"email@email\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"username\",\"Email\":\"email\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"username\",\"Email\":\"email.com\",\"Password\":\"Psswrd\"}")]
    [InlineData("{\"Username\":\"username\",\"Email\":\"email@\",\"Password\":\"Psswrd\"}")]
    public async Task Test_PostUser_Invalid_Request_Invalid_Email_Format(string? requestModel)
    {
        ConfigureObjectValidator();
        var request = JsonConvert.DeserializeObject<UserUpdateRequest>(requestModel!);
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.UpdateUser("Pedro", request!);
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
        var request = JsonConvert.DeserializeObject<UserUpdateRequest>(requestModel!);
        InvalidRequestException? ex = null;
        try
        {
            await this._controller.UpdateUser("Pedro", request!);
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

using Application.UseCases.LoginUser;
using Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.LoginUser;

/// <summary>
/// UsersController
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController(ILoginUserUseCase useCase) : BaseController
{
    /// <summary>
    /// Cria um usuário.
    /// </summary>
    /// <response code="200">Successfull request.</response>
    /// <response code="400">Invalid Request.</response>
    /// <param name="request"></param>
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
    {
        ValidateRequest(request);
        return Ok(await useCase.Execute(request));
    }
    
    
    /// <summary> 
    /// Cria um usuário.
    /// </summary>
    /// <response code="200">Successfull request.</response>
    /// <response code="400">Invalid Request.</response>
    /// <param name="request"></param>
    [HttpPost("Example")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Example([FromBody] LoginUserRequest request)
    {
        ValidateRequest(request);
        await Task.WhenAny();
        return Ok("All is fine");
    }
}

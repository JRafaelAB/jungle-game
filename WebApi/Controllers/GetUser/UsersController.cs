using Application.UseCases.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GetUser;

/// <summary>
/// UsersController
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController(IGetUserUseCase useCase) : BaseController
{
    /// <summary>
    /// Busca um usuário.
    /// </summary>
    /// <response code="200">Success.</response>
    /// <response code="404">User Not Found.</response>
    /// <param name="user"></param>
    [HttpGet("{user}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] string user)
    {
        return Ok(await useCase.Execute(user));
    }
    
    /*
    [HttpGet("helloWorld")]
    [Authorize(Policy = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser()
    {
        return Ok("Hello World");
    }
    */
}

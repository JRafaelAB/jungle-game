using Application.UseCases.CreateUser;
using Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.CreateUser;

/// <summary>
/// UsersController
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController(ICreateUserUseCase createUserUseCase) : BaseController
{
    /// <summary>
    /// Cria um usuário.
    /// </summary>
    /// <response code="201">Ressource Created.</response>
    /// <response code="400">Invalid Request.</response>
    /// <response code="409">Already Existing Login.</response>
    /// <param name="request"></param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        ValidateRequest(request);
        await createUserUseCase.Execute(request);
        
        return Created();
    }
}

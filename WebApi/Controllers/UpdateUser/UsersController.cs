using Application.UseCases.UpdateUser;
using Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.UpdateUser;

[ApiController]
[Route("[controller]")]
public class UsersController (IUpdateUserUseCase useCase) : BaseController
{
    [HttpPost("{user}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser([FromRoute] string user, [FromBody] UserRequest request)
    {
        ValidateRequest(request);
        await useCase.Execute(request, user);

        return NoContent();
    }
}
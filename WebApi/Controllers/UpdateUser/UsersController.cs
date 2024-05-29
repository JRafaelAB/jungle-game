using Application.UseCases.UpdateUser;
using Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.UpdateUser;

[ApiController]
[Route("[controller]")]
public class UsersController (IUpdateUserUseCase useCase) : BaseController
{
    [HttpPost("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser([FromRoute] ulong userId, [FromBody] UserRequest request)
    {
        ValidateRequest(request);
        await useCase.Execute(request, userId);

        return NoContent();
    }
}
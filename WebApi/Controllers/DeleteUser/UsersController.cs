using Application.UseCases.DeleteUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DeleteUser;

[ApiController]
[Route("[controller]")]
public class UsersController (IDeleteUserUseCase useCase) : BaseController
{

    [HttpDelete("{user}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] string user)
    {
        await useCase.Execute(user);
        
        return NoContent();
    }
}
using Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Bets;


[ApiController]
[Route("[controller]")]
public class BetController (IBetUseCase betUseCase) : BaseController
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
    public async Task<IActionResult> Bet([FromBody] BetRequest request)
    {
        ValidateRequest(request);
        ulong id = 1;
        
        await betUseCase.Execute(id, request);
        return Created();
    }
}

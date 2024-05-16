using Domain.Exceptions;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public abstract class BaseController : Controller
{
    protected void ValidateRequest<T>(T request)
    {
        if (request == null || !TryValidateModel(request))
        {
            throw new InvalidRequestException(ModelState.GetErrorList());
        }
    }
    
}

using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using Domain.Exceptions;
using Domain.Resources;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace WebApi.Modules.Middlewares;

[ExcludeFromCodeCoverage]
internal static class ExceptionHandlerMiddleware
{
    public static async Task ExceptionHandler(HttpContext context)
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            
        context.Response.ContentType = MediaTypeNames.Application.Json;
        switch (contextFeature?.Error)
        {
            /*
            case InvalidLoginException invalidLogin:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                logger.Error($"Invalid Login: {JsonConvert.SerializeObject(invalidLogin.notificationError)}");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(invalidLogin.notificationError));
                break;
            */
            
            case LoginConflictException loginConflict:
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                //logger.Error($"Login Conflict: {JsonConvert.SerializeObject(loginConflict.notificationError)}");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(loginConflict));
                break;
            
            
            case InvalidRequestException invalidRequest:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                //logger.Error($"Invalid Request: {JsonConvert.SerializeObject(invalidRequest)}");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(invalidRequest));
                break;
            
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //logger.Error($"Unexpected Error: {contextFeature.Error}");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(Messages.InternalServerError + $" | traceId: {context.TraceIdentifier}"));
                break;
        }
    }
}

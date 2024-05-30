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
            
            case ExceptionBase exception:
                context.Response.StatusCode = exception.HttpStatus;
                //logger.Error($"Invalid Login: {JsonConvert.SerializeObject(exception)}");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
                break;
            
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //logger.Error($"Unexpected Error: {contextFeature.Error}");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(Messages.InternalServerError + $" | traceId: {context.TraceIdentifier}"));
                break;
        }
    }
}

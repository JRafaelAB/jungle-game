using System.Diagnostics.CodeAnalysis;
using Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using WebApi.Modules.Middlewares;
using WebApi.Modules.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSQLServer(builder.Configuration)
    .AddUseCases()
    .AddCronJobs();

Configuration.SetConfiguration(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context => await ExceptionHandlerMiddleware.ExceptionHandler(context));
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
internal partial class Program;

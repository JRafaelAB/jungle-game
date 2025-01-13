using System.Diagnostics.CodeAnalysis;
using Domain.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Modules.Middlewares;
using WebApi.Modules.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSqlServer(builder.Configuration)
    .AddUseCases()
    .AddCronJobs()
    .AddHttpClients(builder.Configuration);

builder.Services.AddCognitoIdentity();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Cognito:Authority"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy => policy.RequireClaim("cognito:groups", "PantryAdmin"));

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
internal partial class Program;

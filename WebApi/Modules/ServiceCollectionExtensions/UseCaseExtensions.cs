using System.Diagnostics.CodeAnalysis;
using Application.UseCases.CreateUser;
using Application.UseCases.GetUser;
using Application.UseCases.LoginUser;
using Application.UseCases.UpdateUser;

namespace WebApi.Modules.ServiceCollectionExtensions;

[ExcludeFromCodeCoverage]
public static class UseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

        return services;
    }
}
﻿using System.Diagnostics.CodeAnalysis;
using Domain.Constants;
using Infrastructure.Services.Lottery;
using RestEase.HttpClientFactory;

namespace WebApi.Modules.ServiceCollectionExtensions;

[ExcludeFromCodeCoverage]
public static class HttpClientExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        string? baseUrl = configuration.GetConnectionString(ConfigurationConstants.LOTTERY_HTTP_CLIENT);
        services.AddRestEaseClient<ILotteryApi>(baseUrl);

        return services;
    }
}
using System.Diagnostics.CodeAnalysis;
using Domain.Constants;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Modules.ServiceCollectionExtensions;

[ExcludeFromCodeCoverage]
internal static class SqlExtensions
{
    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConfigurationConstants.JungleDbConnectionString);
        
        services.AddDbContext<JungleContext>(
            options =>
            {
                options.UseSqlServer(connectionString, option => option.MigrationsAssembly(nameof(Infrastructure)));
            });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILotteryResultsRepository, LotteryResultsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

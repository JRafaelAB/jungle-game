using System.Diagnostics.CodeAnalysis;
using Application.Cronjobs;
using Quartz;

namespace WebApi.Modules.ServiceCollectionExtensions;

[ExcludeFromCodeCoverage]
public static class ConjobExtensions
{
    public static IServiceCollection AddCronJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            var LotteryJobKey = new JobKey(nameof(LotteryJob));
            q.AddJob<LotteryJob>(opts => opts.WithIdentity(LotteryJobKey));

            q.AddTrigger(opts => opts
                .ForJob(LotteryJobKey)
                .WithIdentity(LotteryJobKey.Name + "-trigger")
                .WithCronSchedule("/10 * * ? * *"));

        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}

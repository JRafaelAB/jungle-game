using Quartz;

namespace Application.Cronjobs;

public class LotteryJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"Executing job at {DateTime.Now} - Trigger Key: {context.Trigger.Key.Name} - Trigger Job Key {context.Trigger.JobKey} - Trigger Description {context.Trigger.Description}");
        return Task.FromResult(true);
    }
}

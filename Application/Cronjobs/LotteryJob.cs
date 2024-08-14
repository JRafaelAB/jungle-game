using Domain.Repositories;
using Domain.Services;
using Domain.UnitOfWork;
using Quartz;

namespace Application.Cronjobs;

public class LotteryJob(ILotteryService lotteryService, ILotteryResultsRepository repository, IUnitOfWork unitOfWork) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($@"Executing job at {DateTime.Now} - Trigger Key: {context.Trigger.Key.Name} - Trigger Job Key {context.Trigger.JobKey}");

        var lottery = await lotteryService.GetLotteryResults();
        await repository.AddLotteryResults(lottery);
        await unitOfWork.Save();
        
        Console.WriteLine(lottery.ToString());
    }
}

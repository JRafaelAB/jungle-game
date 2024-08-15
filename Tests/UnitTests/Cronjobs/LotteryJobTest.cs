using Application.Cronjobs;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Domain.UnitOfWork;
using Moq;
using Quartz;
using Xunit;

namespace UnitTests.Cronjobs;

public class LotteryJobTest
{
    private readonly Mock<ILotteryResultsRepository> _repository = new ();
    private readonly Mock<ILotteryService> _service = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly LotteryJob _job;
    private readonly Mock<IJobExecutionContext> _context = new();
    
    public LotteryJobTest()
    {
        this._job = new LotteryJob(this._service.Object, this._repository.Object, this._unitOfWork.Object);
        this._service.Setup(x => x.GetLotteryResults()).ReturnsAsync(new LotteryDto([
            11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30
        ]));
        var trigger = new Mock<ITrigger>();
        this._context.SetupGet(p => p.Trigger).Returns(trigger.Object);
        trigger.SetupGet(p => p.JobKey).Returns(new JobKey("jobKey"));
        trigger.SetupGet(p => p.Key).Returns(new TriggerKey("Key"));
    }
    [Fact]
    public async Task Test_LotteryJob()
    {
        await this._job.Execute(this._context.Object);
        this._service.Verify(x=> x.GetLotteryResults(), Times.Once);
        this._repository.Verify(x=> x.AddLotteryResults(It.IsAny<LotteryDto>()), Times.Once);
        this._unitOfWork.Verify(x=> x.Save(), Times.Once);
    }
}
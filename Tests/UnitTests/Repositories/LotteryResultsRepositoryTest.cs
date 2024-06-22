using Infrastructure.Repositories;
using Xunit;

namespace UnitTests.Repositories;

public class LotteryResultsRepositoryTest
{
    private readonly JungleContextMock contextMock = new ("lottery");
    
    [Fact]
    public async Task Test_Add_New_LotteryResults()
    {
        var context = await contextMock.StartNewContext();
        var repository = new LotteryResultsRepository(context);
        await repository.AddLotteryResults(JungleContextMock.Lottery1Dto);
        await context.SaveChangesAsync();
        Assert.Contains(JungleContextMock.Lottery1, context.LotteryResults);
        await context.DisposeAsync();
    }
}

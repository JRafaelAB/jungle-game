using Infrastructure.Repositories;
using Xunit;

namespace UnitTests.Repositories;

public class LotteryResultsRepositoryTest
{
    [Fact]
    public async Task Test_Add_New_LotteryResults()
    {
        var context = await JungleContextMock.StartNewContext();
        var repository = new LotteryResultsRepository(context);
        await repository.AddLotteryResults(JungleContextMock.Lottery1Dto);
        await context.SaveChangesAsync();
        Assert.Contains(JungleContextMock.Lottery1, context.LotteryResults);
        await context.DisposeAsync();
    }
}

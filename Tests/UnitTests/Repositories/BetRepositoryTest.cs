using Infrastructure.Repositories;
using Xunit;

namespace UnitTests.Repositories
{
    public class BetRepositoryTest
    {
        private readonly JungleContextMock contextMock = new("bet");

        [Fact]
        public async Task Add_New_Bet()
        {
            await using var context = await contextMock.StartNewContext();
            var repository = new BetsRepository(context);
            await repository.AddBets(JungleContextMock.BetUser1Dto);
            await context.SaveChangesAsync();
            Assert.Contains(JungleContextMock.Bet1, context.Bets);
        }
    }
}

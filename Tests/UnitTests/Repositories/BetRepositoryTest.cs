using Infrastructure.Repositories;
using Xunit;

namespace UnitTests.Repositories
{
    public class BetRepositoryTest
    {
        private readonly JungleContextMock _contextMock = new("bet");

        [Fact]
        public async Task Add_New_Bet()
        {
            await using var context = await _contextMock.StartNewContext();
            var repository = new BetsRepository(context);
            await repository.AddBets(JungleContextMock.BetUser1Dto);
            await context.SaveChangesAsync();
            Assert.Equal(JungleContextMock.Bet1, context.Bets.FirstOrDefault());
        }
    }
}

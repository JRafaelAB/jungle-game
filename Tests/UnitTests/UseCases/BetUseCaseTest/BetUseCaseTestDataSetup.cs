using Domain.Models.Requests;

namespace UnitTests.UseCases.BetUseCaseTest;

public static class BetUseCaseTestDataSetup
{
    public static readonly BetRequest validBet = new(01234, 10,Domain.Constants.Enums.BetTypes.Duke, [0,1,2,3], Domain.Constants.Enums.Lotteries.Lottery1);
}

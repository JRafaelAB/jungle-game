using Application.UseCases.Bet;
using Domain.DTOs;
using Domain.Repositories;
using Domain.UnitOfWork;
using Domain.Utils;
using Moq;
using Xunit;

namespace UnitTests.UseCases.BetUseCaseTest;

public class BetUseCaseTest
{
    private readonly Mock<IBetsRepository> _betRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly BetUseCase _useCase;

    public BetUseCaseTest()
    {
        Configuration.SetConfiguration(TestConfigurationBuilder.BuildTestConfiguration());
        this._betRepository = new Mock<IBetsRepository>();
        this._unitOfWork = new Mock<IUnitOfWork>();
        this._useCase = new BetUseCase(_betRepository.Object, _unitOfWork.Object);
    }

    [Fact]

    public async Task Place_New_Bet()
    {
        BetsDto bet = new(01234, new DateTime(2024,10,06), 10, Domain.Constants.Enums.BetTypes.Duke, [0,1,2,3], Domain.Constants.Enums.Lotteries.Lottery1);

        await _useCase.Execute(1,BetUseCaseTestDataSetup.validBet);

        Assert.Equal(bet, bet);
        this._betRepository.Verify(repo => repo.AddBets(bet), Times.Once);
        this._unitOfWork.Verify(x => x.Save(), Times.Once);
    }
}

using Domain.DTOs;

namespace Domain.Repositories;

public interface ILotteryResultsRepository
{
    Task AddLotteryResults(LotteryDTO lotteryDto);
}

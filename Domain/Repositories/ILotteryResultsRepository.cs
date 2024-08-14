using Domain.DTOs;

namespace Domain.Repositories;

public interface ILotteryResultsRepository
{
    Task AddLotteryResults(LotteryDto lotteryDto);
}

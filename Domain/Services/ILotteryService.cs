using Domain.DTOs;

namespace Domain.Services;

public interface ILotteryService
{
    Task<LotteryDto> GetLotteryResults();
}

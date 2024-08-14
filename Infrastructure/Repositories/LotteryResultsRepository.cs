using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DataAccess.Contexts;

namespace Infrastructure.Repositories;

public class LotteryResultsRepository(JungleContext context) : ILotteryResultsRepository
{
    public async Task AddLotteryResults(LotteryDto lotteryDto)
    {
        LotteryResults lotteryEntity = new(lotteryDto);
        await context.LotteryResults.AddAsync(lotteryEntity);
    }
}

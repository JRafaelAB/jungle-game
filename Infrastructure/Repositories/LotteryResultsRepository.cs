using Domain.DTOs;
using Domain.Entities;
using Infrastructure.DataAccess.Contexts;

namespace Infrastructure.Repositories;

public class LotteryResultsRepository(JungleContext context)
{
    public async Task AddUser(LotteryDTO lotteryDto)
    {
        LotteryResults lotteryEntity = new(lotteryDto);
        await context.LotteryResults.AddAsync(lotteryEntity);
    } 
}

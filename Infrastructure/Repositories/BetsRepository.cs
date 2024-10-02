using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DataAccess.Contexts;

namespace Infrastructure.Repositories;

public class BetsRepository(JungleContext context) : IBetsRepository
{
    public async Task AddBets(BetsDto betsDto)
    {
        var betEntity = new Bets(betsDto);
        await context.Bets.AddAsync(betEntity);
    }
}

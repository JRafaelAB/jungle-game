using Domain.DTOs;

namespace Domain.Repositories;

public interface IBetsRepository
{
    public Task AddBets(BetsDto bets);
}

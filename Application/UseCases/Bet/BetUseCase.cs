using Domain.DTOs;
using Domain.Models.Requests;
using Domain.Repositories;
using Domain.UnitOfWork;

namespace Application.UseCases.Bet;

public class BetUseCase (IBetsRepository betRepository, IUnitOfWork unitOfWord) : IBetUseCase
{
    public async Task Execute(ulong id, BetRequest request)
    {
        BetsDto bet = new(id, request);
        await betRepository.AddBets(bet);
        await unitOfWord.Save();
    }
}

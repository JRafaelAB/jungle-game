using Domain.Models.Requests;

public interface IBetUseCase
{
    public Task Execute(ulong id,BetRequest request);
}

using static Domain.Constants.Enums;

namespace Domain.Entities;

public class Bets
{
    public ulong? Id { get; set; }
    public ulong UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public BetTypes Type { get; set; }
    public string? Data { get; set; }
    public Lotteries LotteryNumber { get; set; }
}
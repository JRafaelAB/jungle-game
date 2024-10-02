using Domain.DTOs;
using static Domain.Constants.Enums;

namespace Domain.Entities;

public class Bets (ulong userId, DateTime date, decimal value, BetTypes type, uint[] data, Lotteries lotteryNumber, ulong? id = null)
{
    public ulong? Id { get; set; } = id;
    public ulong UserId { get; set; } = userId;
    public DateTime Date { get; set; } = date;
    public decimal Value { get; set; } = value;
    public BetTypes Type { get; set; } = type;
    public uint[] Data { get; set; } = data;
    public Lotteries LotteryNumber { get; set; } = lotteryNumber;

    

    public Bets(BetsDto betsDto) : this(betsDto.UserId, betsDto.Date, betsDto.Value, betsDto.Type, betsDto.Data, betsDto.LotteryNumber,betsDto.Id) { }

    public override bool Equals(object? obj)
    {
        return obj is Bets bets &&
               UserId == bets.UserId &&
               Date == bets.Date &&
               Value == bets.Value &&
               Type == bets.Type &&
               EqualityComparer<uint[]>.Default.Equals(Data, bets.Data) &&
               LotteryNumber == bets.LotteryNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, Date, Value, Type, Data, LotteryNumber);
    }
}
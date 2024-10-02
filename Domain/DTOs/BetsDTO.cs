using Domain.Entities;
using Domain.Models.Requests;
using static Domain.Constants.Enums;

namespace Domain.DTOs;

public class BetsDto(ulong userId, DateTime date, decimal value, BetTypes type, uint[] data, Lotteries lotteryNumber)
{
    public ulong? Id { get; set; }
    public ulong UserId { get; set; } = userId;
    public DateTime Date { get; set; } = date;
    public decimal Value { get; set; } = value;
    public BetTypes Type { get; set; } = type;
    public uint[] Data { get; set; } = data;
    public Lotteries LotteryNumber { get; set; } = lotteryNumber;

    public BetsDto(Bets bets) : this(bets.UserId, bets.Date, bets.Value, bets.Type, bets.Data, bets.LotteryNumber)
    {
        this.Id = bets.Id;
    }
    public BetsDto(ulong id, BetRequest bets) : this(id, bets.Date, bets.Value, bets.Type, bets.Data, bets.LotteryNumber)
    {
    }

    public override bool Equals(object? obj)
    {
        return obj is BetsDto dto &&
               UserId == dto.UserId &&
               Date == dto.Date &&
               Value == dto.Value &&
               Type == dto.Type &&
               EqualityComparer<uint[]>.Default.Equals(Data, dto.Data) &&
               LotteryNumber == dto.LotteryNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, Date, Value, Type, Data, LotteryNumber);
    }
}

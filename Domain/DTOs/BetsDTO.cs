using Domain.Entities;
using Domain.Models.Requests;
using static Domain.Constants.Enums;

namespace Domain.DTOs;

public class BetsDto
{
    public ulong? Id { get; set; }
    public ulong UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public BetTypes Type { get; set; }
    public uint[] Data { get; set; }
    public Lotteries LotteryNumber { get; set; }

    public BetsDto(ulong UserId, DateTime Date, decimal Value, BetTypes Type, uint[] Data, Lotteries LotteryNumber, ulong? Id)
    {
        this.Id = Id;
        this.UserId = UserId;
        this.Date = Date;
        this.Value = Value;
        this.Type = Type;
        this.Data = Data;
        this.LotteryNumber = LotteryNumber;
    }

    public BetsDto(Bets bets)
    {
        this.Id = bets.Id;
        this.UserId = bets.UserId;
        this.Date = bets.Date;
        this.Value = bets.Value;
        this.Type = bets.Type;
        this.Data = bets.Data;
        this.LotteryNumber = bets.LotteryNumber;
    }
    public BetsDto(ulong id, BetRequest bets)
    {
        this.UserId = id;
        this.Date = bets.Date;
        this.Value = bets.Value;
        this.Type = bets.Type;
        this.Data = bets.Data;
        this.LotteryNumber = bets.LotteryNumber;
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

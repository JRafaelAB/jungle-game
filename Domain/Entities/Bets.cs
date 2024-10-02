using Domain.DTOs;
using Domain.Extensions;
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
    
    protected bool Equals(Bets other)
    {
        return UserId == other.UserId && Date.Equals(other.Date) && Value == other.Value && Type == other.Type && Data.EqualsTo(other.Data) && LotteryNumber == other.LotteryNumber;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Bets)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, Date, Value, (int)Type, Data, (int)LotteryNumber);
    }

    public static bool operator ==(Bets? left, Bets? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Bets? left, Bets? right)
    {
        return !Equals(left, right);
    }
    
}
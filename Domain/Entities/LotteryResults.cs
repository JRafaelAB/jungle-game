using Domain.DTOs;

namespace Domain.Entities;

public class LotteryResults(uint numbersPerLottery, string? lottery1, string? lottery2, string? lottery3, string? 
        lottery4, string? lottery5, DateTime createdAt)
{
    public uint Id { get; init; }
    public uint NumbersPerLottery { get; init; } = numbersPerLottery;
    public string? Lottery1 { get; init; } = lottery1;
    public string? Lottery2 { get; init; } = lottery2;
    public string? Lottery3 { get; init; } = lottery3;
    public string? Lottery4 { get; init; } = lottery4;
    public string? Lottery5 { get; init; } = lottery5;

    public DateTime CreatedAt { get; private set; } = createdAt.ToUniversalTime();

    public LotteryResults(LotteryDto dto) : this(dto.NumbersPerLottery, dto.Lottery1, dto.Lottery2, dto.Lottery3,
        dto.Lottery4, dto.Lottery5, dto.CreatedAt) {}
    
    
    protected bool Equals(LotteryResults other)
    {
        return Id == other.Id && NumbersPerLottery == other.NumbersPerLottery && Lottery1 == other.Lottery1 && Lottery2 == other.Lottery2 && Lottery3 == other.Lottery3 && Lottery4 == other.Lottery4 && Lottery5 == other.Lottery5;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((LotteryResults)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, NumbersPerLottery, Lottery1, Lottery2, Lottery3, Lottery4, Lottery5);
    }
}

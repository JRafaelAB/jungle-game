using Domain.DTOs;

namespace Domain.Entities;

public class LotteryResults(uint numbersPerLottery, string? lottery1, string? lottery2, string? lottery3, string? 
        lottery4)
{
    public uint Id { get; init; }
    public uint NumbersPerLottery { get; init; } = numbersPerLottery;
    public string? Lottery1 { get; init; } = lottery1;
    public string? Lottery2 { get; init; } = lottery2;
    public string? Lottery3 { get; init; } = lottery3;
    public string? Lottery4 { get; init; } = lottery4;

    public LotteryResults(LotteryDTO dto) : this(dto.NumbersPerLottery, dto.Lottery1, dto.Lottery2, dto.Lottery3,
        dto.Lottery4) {}
}

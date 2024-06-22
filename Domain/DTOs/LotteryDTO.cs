using Domain.Extensions;

namespace Domain.DTOs;

public class LotteryDTO
{
    public uint NumbersPerLottery { get; }
    public string? Lottery1 { get; }
    public string? Lottery2 { get; }
    public string? Lottery3 { get; }
    public string? Lottery4 { get; }
    public string? Lottery5 { get; }
    
    public DateTime CreatedAt { get; } = DateTime.Now.ToUniversalTime();

    public LotteryDTO(uint[] numbers, uint numbersPerLottery = 4)
    {
        var filteredNumbers = numbers.Select(number => number % 10).ToArray();
        this.NumbersPerLottery = numbersPerLottery;
        
        if (filteredNumbers.Length < numbersPerLottery * 5) throw new ArgumentOutOfRangeException(nameof(filteredNumbers));
        
        this.Lottery1 = EnumerableToLottery(0, filteredNumbers);
        this.Lottery2 = EnumerableToLottery(numbersPerLottery, filteredNumbers);
        this.Lottery3 = EnumerableToLottery(numbersPerLottery*2, filteredNumbers);
        this.Lottery4 = EnumerableToLottery(numbersPerLottery*3, filteredNumbers);
        this.Lottery5 = EnumerableToLottery(numbersPerLottery*5, filteredNumbers);

    }
    private string EnumerableToLottery(uint offset, uint[] numbers)
    {
        return $"{numbers[0+offset]}-{numbers[0+offset]},{numbers[0+offset]}-{numbers[0+offset]}";
    }

    public override string ToString()
    {
        return $"Lottery Results:/n" +
               $"1st Lottery: {Lottery1?.GetOnlyNumbers()}/n" +
               $"2nd Lottery: {Lottery2?.GetOnlyNumbers()}/n" +
               $"3rd Lottery: {Lottery3?.GetOnlyNumbers()}/n" +
               $"4th Lottery: {Lottery4?.GetOnlyNumbers()}/n" +
               $"5th Lottery: {Lottery5?.GetOnlyNumbers()}";
    }
}

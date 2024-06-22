namespace Domain.DTOs;

public class LotteryDTO
{
    public uint NumbersPerLottery { get; set; }
    public string? Lottery1 { get; set; }
    public string? Lottery2 { get; set; }
    public string? Lottery3 { get; set; }
    public string? Lottery4 { get; set; }
    
    public DateTime CreatedAt { get; } = DateTime.Now.ToUniversalTime();

    public LotteryDTO(uint[] numbers, uint numbersPerLottery = 4)
    {
        var filteredNumbers = numbers.Select(number => number % 10).ToArray();
        this.NumbersPerLottery = numbersPerLottery;
        
        if (filteredNumbers.Length < numbersPerLottery * 4) throw new ArgumentOutOfRangeException(nameof(filteredNumbers));
        
        this.Lottery1 = EnumerableToLottery(0, filteredNumbers);
        this.Lottery2 = EnumerableToLottery(numbersPerLottery, filteredNumbers);
        this.Lottery3 = EnumerableToLottery(numbersPerLottery*2, filteredNumbers);
        this.Lottery4 = EnumerableToLottery(numbersPerLottery*3, filteredNumbers);

    }
    private string EnumerableToLottery(uint offset, uint[] numbers)
    {
        return $"{numbers[0+offset]}-{numbers[0+offset]},{numbers[0+offset]}-{numbers[0+offset]}";
    }
}

using Domain.Resources;
using System.ComponentModel.DataAnnotations;
using static Domain.Constants.Enums;

namespace Domain.Models.Requests;

public class BetRequest (ulong UserId, decimal value, BetTypes type, uint[] data, Lotteries lotteryNumber) : IValidatableObject
{
    [Required]
    public ulong UserId { get; set; } = UserId;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    [Required]
    public decimal Value { get; set; } = value;
    [Required]
    public BetTypes Type { get; set; } = type;
    [Required]
    public uint[] Data { get; set; } = data;
    [Required]
    public Lotteries LotteryNumber { get; set; } = lotteryNumber;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        switch (this.Type)
        {
            case (BetTypes.Group):
                if (Data.Length != 1 || Data[0] == 0 || Data[0] > 25)
                    Message(errors);
                break;
            case (BetTypes.Tenth):
                if (Data.Length != 1 || Data[0] > 100)
                    Message(errors);
                break;
            case (BetTypes.Hundred):
                if (Data.Length != 0 || Data[0] > 1000)
                    Message(errors);
                break;
            case (BetTypes.Thousand):
                if(Data.Length != 1 || Data[0] > 10000)
                    Message(errors);
                break;
            case (BetTypes.Duke):
                if (Data.Length != 2 || Index(Data, 100));
                    Message(errors);
                break;
            case (BetTypes.King):
                if(Data.Length != 3 || Index(Data, 10));
                    Message(errors);
                break;
            case (BetTypes.Couple):
                if(Data.Length != 2 || Data[0] == 0 || Data[1] == 0 || Index(Data, 25))
                    Message(errors);
                break;
            case (BetTypes.Triple):
                if(Data.Length != 3 || Data[0] == 0 || Data[1] == 0 || Data[2] == 0 || Index(Data, 25))
                    Message(errors);
                break;

        }
        return errors;

        
        
    }
    private static void Message(List<ValidationResult> errors)
    {
        errors.Add(new ValidationResult(Messages.InvalidBet));
    }

    private static bool Index(uint[] data, int condicao)
    {
        foreach (var item in data)
        {
            if (item > condicao)
                return false;
        }
        return true;
    }
}

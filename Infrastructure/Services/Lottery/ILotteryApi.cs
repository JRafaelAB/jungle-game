using RestEase;

namespace Infrastructure.Services.Lottery;

public interface ILotteryApi
{
    [Header("x-api-key")]
    string? ApiKey { get; set; }
    
    [Get("?length=20&type=uint8")]
    Task<HttpResponseMessage> FetchLotteryNumbersAsync();
}

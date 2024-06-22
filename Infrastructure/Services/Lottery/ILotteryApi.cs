using RestEase;

namespace Infrastructure.Services.Lottery;

public interface ILotteryApi
{
    [Header("x-api-key")]
    string? ApiKey { set; }
    
    [Get]
    Task<HttpResponseMessage> FetchLotteryNumbersAsync([Query("length")] int length=20, [Query("type")] string type="uint8");
}

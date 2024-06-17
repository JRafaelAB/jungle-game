using RestEase;

namespace Infrastructure.Services.Lottery;

public interface ILotteryApi
{
    [Get]
    Task<HttpResponseMessage> FetchLotteryNumbersAsync([Query("length")] int length=10, [Query("type")] string type="uint8");
}

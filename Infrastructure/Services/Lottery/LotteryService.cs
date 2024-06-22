using Domain.Constants;
using Domain.DTOs;
using Domain.Extensions;
using Domain.Models.Responses;
using Domain.Services;
using Domain.Utils;
using Newtonsoft.Json;

namespace Infrastructure.Services.Lottery;

public class LotteryService : ILotteryService
{
   private readonly ILotteryApi _lotteryApi;
   
   public LotteryService(ILotteryApi lotteryApi)
   {
      this._lotteryApi = lotteryApi;
      this._lotteryApi.ApiKey = Configuration.GetConfigurationValue<string>(ConfigurationConstants.LOTTERY_API_KEY);
   }
   
   public async Task<LotteryDTO> GetLotteryResults()
   {
      var response = await this._lotteryApi.FetchLotteryNumbersAsync();

      if (!response.IsSuccessStatusCode) throw new SystemException(response.Content.ToString());

      var lotteryResponse = JsonConvert.DeserializeObject<GetLotteryResponse>(response.Content.ToStringOrEmpty());

      if (lotteryResponse?.Data == null) throw new ArgumentException(nameof(lotteryResponse));

      return new LotteryDTO(lotteryResponse.Data);
   }
}

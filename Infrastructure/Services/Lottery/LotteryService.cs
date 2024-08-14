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
      this._lotteryApi.ApiKey = Configuration.GetConfigurationValue<string>(ConfigurationConstants.LotteryApiKey);
   }
   
   public async Task<LotteryDto> GetLotteryResults()
   {
      var response = await this._lotteryApi.FetchLotteryNumbersAsync();

      if (!response.IsSuccessStatusCode) throw new SystemException(response.Content.ToString());

      var lotteryResponse = JsonConvert.DeserializeObject<GetLotteryResponse>(await response.Content.ReadAsStringAsync());

      if (lotteryResponse?.Data == null) throw new ArgumentException(nameof(lotteryResponse));

      return new LotteryDto(lotteryResponse.Data);
   }
}

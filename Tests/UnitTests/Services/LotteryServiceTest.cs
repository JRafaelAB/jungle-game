using System.Net;
using Domain.Models.Responses;
using Domain.Services;
using Domain.Utils;
using Infrastructure.Services.Lottery;
using Moq;
using Xunit;
using Newtonsoft;
using Newtonsoft.Json;

namespace UnitTests.Services;

public class LotteryServiceTest
{
    private readonly Mock<ILotteryApi> _apiMock = new Mock<ILotteryApi>();
    private readonly ILotteryService _service;
    private readonly GetLotteryResponse _response = new GetLotteryResponse()
    {
        Success = true,
        Type = "type",
        Length = "10",
        Data = [11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30]
    };
    
    private readonly GetLotteryResponse _emptyDataResponse = new GetLotteryResponse()
    {
        Success = true,
        Type = "type",
        Length = "10",
        Data = []
    };
    
    private readonly GetLotteryResponse _nullDataResponse = new GetLotteryResponse()
    {
        Success = true,
        Type = "type",
        Length = "10",
        Data = null
    };

    public LotteryServiceTest()
    {
        Configuration.SetConfiguration(TestConfigurationBuilder.BuildTestConfiguration());
        _service = new LotteryService(_apiMock.Object);
    }
    
    [Fact]
    public async Task Test_Success_Get_Results()
    {
        MockConfigSuccess();
        var result = await this._service.GetLotteryResults();
        Assert.NotNull(result);
        Assert.Equal("1-2,3-4", result.Lottery1);
        Assert.Equal("5-6,7-8", result.Lottery2);
        Assert.Equal("9-0,1-2", result.Lottery3);
        Assert.Equal("3-4,5-6", result.Lottery4);
        Assert.Equal("7-8,9-0", result.Lottery5);
    }
    
    [Fact]
    public async Task Test_Fail_Get_Results()
    {
        MockConfigFail();
        await Assert.ThrowsAsync<SystemException>(() => this._service.GetLotteryResults());
    }
    
    [Fact]
    public async Task Test_Empty_Data_Get_Results()
    {
        MockConfigEmptyData();
        await Assert.ThrowsAsync<ArgumentException>(() => this._service.GetLotteryResults());
    }
    
    [Fact]
    public async Task Test_Null_Data_Get_Results()
    {
        MockConfigNullData();
        await Assert.ThrowsAsync<ArgumentException>(() => this._service.GetLotteryResults());
    }
    
    private void MockConfigSuccess()
    {
        this._apiMock.Setup(mock => 
            mock.FetchLotteryNumbersAsync()
            ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(this._response))
                }
            );
    }
    
    private void MockConfigEmptyData()
    {
        this._apiMock.Setup(mock => 
            mock.FetchLotteryNumbersAsync()
        ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(this._emptyDataResponse))
            }
        );
    }
    
    private void MockConfigNullData()
    {
        this._apiMock.Setup(mock => 
            mock.FetchLotteryNumbersAsync()
        ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(this._nullDataResponse))
            }
        );
    }
    
    private void MockConfigFail()
    {
        this._apiMock.Setup(mock => 
            mock.FetchLotteryNumbersAsync()
        ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(string.Empty)
            }
        );
    }
}
﻿namespace Domain.Models.Responses;

public class GetLotteryResponse
{
    public bool Success { get; set; }
    public string? Type { get; set; }
    public string? Length { get; set; }
    public IEnumerable<uint>? Data { get; set; }
}

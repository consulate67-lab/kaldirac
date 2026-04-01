using System;

namespace AntigravityAiTraderV2.Core.Entities;

public class MarketData
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Volume { get; set; }
    public DateTime Timestamp { get; set; }
}

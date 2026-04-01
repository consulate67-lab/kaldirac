using AntigravityAiTraderV2.Core.Entities;
using System.Collections.Generic;

namespace AntigravityAiTraderV2.Application.Interfaces;

public interface IIndicatorService
{
    decimal CalculateRsi(IEnumerable<MarketData> data, int period = 14);
    (decimal MacdLine, decimal SignalLine, decimal Histogram) CalculateMacd(IEnumerable<MarketData> data, int shortPeriod = 12, int longPeriod = 26, int signalPeriod = 9);
    decimal CalculateTrend(IEnumerable<MarketData> data);
}

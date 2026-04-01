using AntigravityAiTraderV2.Application.Interfaces;
using AntigravityAiTraderV2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntigravityAiTraderV2.Application.Services;

public class IndicatorService : IIndicatorService
{
    public decimal CalculateRsi(IEnumerable<MarketData> data, int period = 14)
    {
        var list = data.OrderBy(x => x.Timestamp).ToList();
        if (list.Count < period + 1) return 50m; // Non-decisive if not enough data

        decimal gainSum = 0;
        decimal lossSum = 0;

        for (int i = 1; i <= period; i++)
        {
            var diff = list[i].Price - list[i - 1].Price;
            if (diff > 0) gainSum += diff;
            else lossSum -= diff; // keep positive
        }

        var avgGain = gainSum / period;
        var avgLoss = lossSum / period;

        if (avgLoss == 0) return 100m;

        for (int i = period + 1; i < list.Count; i++)
        {
            var diff = list[i].Price - list[i - 1].Price;
            var gain = diff > 0 ? diff : 0m;
            var loss = diff < 0 ? -diff : 0m;

            avgGain = ((avgGain * (period - 1)) + gain) / period;
            avgLoss = ((avgLoss * (period - 1)) + loss) / period;
        }

        if (avgLoss == 0) return 100m;
        
        var rs = avgGain / avgLoss;
        return 100m - (100m / (1m + rs));
    }

    public (decimal MacdLine, decimal SignalLine, decimal Histogram) CalculateMacd(IEnumerable<MarketData> data, int shortPeriod = 12, int longPeriod = 26, int signalPeriod = 9)
    {
        // For simplicity, returning mock MACD logic or a simplified version
        // Proper MACD needs EMA calculations over the series.
        var list = data.OrderBy(x => x.Timestamp).ToList();
        if (list.Count < longPeriod + signalPeriod) return (0m, 0m, 0m);

        // This is a placeholder for actual complex TA logic to keep example concise.
        // We'll simulate a basic diff for the last few items
        var lastPrice = list.Last().Price;
        var prevPrice = list[list.Count - 2].Price;
        
        var macdLine = (lastPrice - prevPrice) * 0.5m; // mock logic
        var signalLine = macdLine * 0.8m; // mock logic
        var histogram = macdLine - signalLine;

        return (macdLine, signalLine, histogram);
    }

    public decimal CalculateTrend(IEnumerable<MarketData> data)
    {
        var list = data.OrderByDescending(x => x.Timestamp).Take(5).ToList();
        if (list.Count < 5) return 0m;

        decimal trend = 0;
        if (list[0].Price > list[4].Price) trend = 1m; // Upward
        else if (list[0].Price < list[4].Price) trend = -1m; // Downward
        return trend;
    }
}

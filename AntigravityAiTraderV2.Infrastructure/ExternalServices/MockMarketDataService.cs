using AntigravityAiTraderV2.Application.Interfaces;
using AntigravityAiTraderV2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Infrastructure.ExternalServices;

public class MockMarketDataService : IMarketDataCollector
{
    private readonly Random _random = new Random();
    private readonly List<string> _watchList = new List<string> { "GARAN.V", "AKBNK.V", "THYAO.V" };

    public Task<IEnumerable<MarketData>> FetchMarketDataAsync()
    {
        var dataList = new List<MarketData>();

        foreach (var symbol in _watchList)
        {
            var data = new MarketData
            {
                Symbol = symbol,
                Price = (decimal)(_random.NextDouble() * 10) + 1m, // Random price 1 to 11
                Volume = _random.Next(1000, 50000),
                Timestamp = DateTime.UtcNow
            };
            dataList.Add(data);
        }

        return Task.FromResult<IEnumerable<MarketData>>(dataList);
    }
}

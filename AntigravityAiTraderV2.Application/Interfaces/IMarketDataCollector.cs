using AntigravityAiTraderV2.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Application.Interfaces;

public interface IMarketDataCollector
{
    Task<IEnumerable<MarketData>> FetchMarketDataAsync();
}

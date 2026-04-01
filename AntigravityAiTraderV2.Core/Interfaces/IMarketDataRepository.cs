using AntigravityAiTraderV2.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Core.Interfaces;

public interface IMarketDataRepository : IGenericRepository<MarketData>
{
    Task<IEnumerable<MarketData>> GetLatestDataBySymbolAsync(string symbol, int count);
}

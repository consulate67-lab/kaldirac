using AntigravityAiTraderV2.Core.Entities;
using AntigravityAiTraderV2.Core.Interfaces;
using AntigravityAiTraderV2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Infrastructure.Repositories;

public class MarketDataRepository : GenericRepository<MarketData>, IMarketDataRepository
{
    private readonly StackExchange.Redis.IConnectionMultiplexer _redis;

    public MarketDataRepository(AppDbContext context, StackExchange.Redis.IConnectionMultiplexer redis) : base(context)
    {
        _redis = redis;
    }

    public override async Task AddAsync(MarketData entity)
    {
        await base.AddAsync(entity);

        // Redis'e son fiyatı kaydet (Cache son fiyatlar)
        var db = _redis.GetDatabase();
        await db.StringSetAsync($"latest_price:{entity.Symbol}", entity.Price.ToString());
    }

    public async Task<IEnumerable<MarketData>> GetLatestDataBySymbolAsync(string symbol, int count)
    {
        return await _dbSet
            .Where(m => m.Symbol == symbol)
            .OrderByDescending(m => m.Timestamp)
            .Take(count)
            .ToListAsync();
    }
}

using AntigravityAiTraderV2.Core.Entities;
using AntigravityAiTraderV2.Core.Interfaces;
using AntigravityAiTraderV2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Infrastructure.Repositories;

public class SignalRepository : GenericRepository<Signal>, ISignalRepository
{
    public SignalRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Signal>> GetLatestSignalsAsync(int count)
    {
        return await _dbSet
            .OrderByDescending(s => s.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}

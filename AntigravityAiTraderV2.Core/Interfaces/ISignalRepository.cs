using AntigravityAiTraderV2.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Core.Interfaces;

public interface ISignalRepository : IGenericRepository<Signal>
{
    Task<IEnumerable<Signal>> GetLatestSignalsAsync(int count);
}

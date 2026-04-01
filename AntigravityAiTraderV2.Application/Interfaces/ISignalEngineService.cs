using AntigravityAiTraderV2.Core.Entities;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Application.Interfaces;

public interface ISignalEngineService
{
    Task<Signal?> AnalyzeAndGenerateSignalAsync(string symbol);
}

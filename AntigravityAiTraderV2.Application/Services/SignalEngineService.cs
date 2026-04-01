using AntigravityAiTraderV2.Application.Interfaces;
using AntigravityAiTraderV2.Core.Entities;
using AntigravityAiTraderV2.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Application.Services;

public class SignalEngineService : ISignalEngineService
{
    private readonly IMarketDataRepository _marketDataRepository;
    private readonly ISignalRepository _signalRepository;
    private readonly IIndicatorService _indicatorService;
    private readonly IAiEngineService _aiEngineService;

    public SignalEngineService(
        IMarketDataRepository marketDataRepository,
        ISignalRepository signalRepository,
        IIndicatorService indicatorService,
        IAiEngineService aiEngineService)
    {
        _marketDataRepository = marketDataRepository;
        _signalRepository = signalRepository;
        _indicatorService = indicatorService;
        _aiEngineService = aiEngineService;
    }

    public async Task<Signal?> AnalyzeAndGenerateSignalAsync(string symbol)
    {
        // Get last 50 market data points to calculate indicators properly
        var dataList = (await _marketDataRepository.GetLatestDataBySymbolAsync(symbol, 50)).ToList();
        
        // Need minimum data points for indicator logic
        if (dataList.Count < 27) return null;

        var rsi = _indicatorService.CalculateRsi(dataList, 14);
        var macdResult = _indicatorService.CalculateMacd(dataList);
        var trend = _indicatorService.CalculateTrend(dataList);

        var score = _aiEngineService.CalculateScore(rsi, macdResult.Histogram, trend);
        var signalType = _aiEngineService.DecideSignal(score);

        // Map score to confidence percentage intuitively
        // Score 7 = 100%, Score 0 = 0%
        // Max possible score is 7
        decimal confidence = (score / 7m) * 100m;

        var signal = new Signal
        {
            Symbol = symbol,
            SignalType = signalType,
            Confidence = confidence,
            CreatedAt = DateTime.UtcNow
        };

        await _signalRepository.AddAsync(signal);
        
        return signal;
    }
}

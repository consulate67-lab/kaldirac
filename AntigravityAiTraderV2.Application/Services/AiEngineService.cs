using AntigravityAiTraderV2.Application.Interfaces;
using AntigravityAiTraderV2.Core.Enums;
using System;

namespace AntigravityAiTraderV2.Application.Services;

public class AiEngineService : IAiEngineService
{
    public int CalculateScore(decimal rsi, decimal macdHistogram, decimal trend)
    {
        int score = 0;

        // RSI < 30 -> +2 puan
        if (rsi < 30m)
            score += 2;
        
        // MACD pozitif -> +2 puan
        if (macdHistogram > 0)
            score += 2;
        
        // Trend yukarı (trend value > 0) -> +3 puan
        if (trend > 0)
            score += 3;

        return score;
    }

    public SignalType DecideSignal(int score)
    {
        if (score >= 5)
            return SignalType.Al;
        if (score >= 3 && score < 5)
            return SignalType.Bekle;
        
        return SignalType.Sat;
    }
}

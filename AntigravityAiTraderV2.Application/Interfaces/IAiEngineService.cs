using AntigravityAiTraderV2.Core.Enums;
using System.Collections.Generic;

namespace AntigravityAiTraderV2.Application.Interfaces;

public interface IAiEngineService
{
    int CalculateScore(decimal rsi, decimal macdHistogram, decimal trend);
    SignalType DecideSignal(int score);
}
